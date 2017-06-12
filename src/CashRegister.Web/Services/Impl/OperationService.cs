using System;
using System.Collections.Generic;
using System.Linq;
using CashRegister.Web.Models;
using CashRegister.Web.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CashRegister.Web.DataAccess;
using CashRegister.Web.Models.DbContext;

namespace CashRegister.Web.Services.Impl
{
    public class OperationService : IOperationService
    {
        private readonly ApplicationDbContext _context;
        private readonly OperationSettings _settings;
        private readonly ICashRegistrationService _cashService;

        public OperationService(IOptions<OperationSettings> options, ApplicationDbContext context, ICashRegistrationService cashService)
        {
            _context = context;
            _settings = options.Value;
            _cashService = cashService;
        }

        public Operation CreateOrUpdate(Operation operation)
        {
            if (operation.Id <= 0)
            {
                operation.Moment = DateTime.Now;
                _context.Operations.Add(operation);
            }
            else
            {
                _context.OperationDetails.AttachRange(operation.Details.Where(x => x.Id != 0));

                var dbOperation = _context.Operations.Include(x => x.Details)
                    .FirstOrDefault(x => x.Id == operation.Id);

                dbOperation.Details.Clear();
                foreach (var detail in operation.Details)
                {
                    dbOperation.Details.Add(detail);
                }
            }

            _context.SaveChanges();

            return operation;
        }

        public IEnumerable<Operation> GetDailyOperations()
        {
            var dayStartHour = _settings.DayStartHour;
            var now = DateTime.Now;
            var start = now.TimeOfDay > TimeSpan.FromHours(dayStartHour) ? now.Date.AddHours(dayStartHour) : now.Date.Subtract(TimeSpan.FromHours(24 - dayStartHour));
            var end = start.AddHours(24);
            var operations = _context.Operations.Include(x => x.Details)
                .Where(x => x.Moment >= start && x.Moment < end)
                .ToList();
            return operations;
        }

        public IEnumerable<OperationGroupModel> GetOperationsByDay(DateTime startDay, DateTime endDay)
        {
            var dayStartHour = _settings.DayStartHour;
            startDay = startDay.Date.AddHours(dayStartHour);
            endDay = endDay.Date.AddHours(24 + dayStartHour);

            var operations = _context.Operations
                .Include(x => x.Details)
                .Where(x => x.Moment >= startDay && x.Moment <= endDay)
                .OrderByDescending(x => x.Moment)
                .ToList();

            if (!operations.Any())
                yield break;

            var firstDateTime = operations.FirstOrDefault().Moment;

            var tempDate = firstDateTime.Hour < dayStartHour
                    ? firstDateTime.Date.Subtract(TimeSpan.FromDays(1))
                    : firstDateTime.Date;
            var cash = _cashService.GetCashRegistrations(tempDate);
            var group = new OperationGroupModel {
                Date = tempDate,
                StartCash = cash.Item1,
                EndCash = cash.Item2
            };

            yield return group;

            for (var i = 0; i < operations.Count; i++)
            {
                var current = operations[i];
                var previous = i > 0 ? operations[i - 1] : null;
                if (previous == null)
                {
                    group.Operations.Add(current);
                    continue;
                }

                if (current.Moment.Hour < dayStartHour && previous.Moment.Hour >= dayStartHour)
                {
                    tempDate = current.Moment.Date.Subtract(TimeSpan.FromDays(1));
                    cash = _cashService.GetCashRegistrations(tempDate);
                    group = new OperationGroupModel {
                        Date = tempDate,
                        StartCash = cash.Item1,
                        EndCash = cash.Item2
                    };
                    yield return group;
                }
                else if (current.Moment.Date < previous.Moment.Date && group.Date > current.Moment.Date)
                {
                    tempDate = current.Moment.Hour < dayStartHour
                            ? current.Moment.Date.Subtract(TimeSpan.FromDays(1))
                            : current.Moment.Date;
                    cash = _cashService.GetCashRegistrations(tempDate);
                    group = new OperationGroupModel {
                        Date = tempDate,
                        StartCash = cash.Item1,
                        EndCash = cash.Item2
                    };
                    yield return group;
                }

                //if (current.Moment.Hour > dayStartHour && current.Moment.Minute > 0 && previous.Moment.Hour < dayStartHour)
                //{
                //    group = new OperationGroupViewModel { Date = current.Moment.Date };
                //    yield return group;
                //}

                group.Operations.Add(current);
            }
        }
    }
}
