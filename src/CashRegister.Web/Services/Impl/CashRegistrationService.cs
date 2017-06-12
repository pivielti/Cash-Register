using CashRegister.Web.DataAccess;
using CashRegister.Web.Models.DbContext;
using CashRegister.Web.Models.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace CashRegister.Web.Services.Impl
{
    public class CashRegistrationService : ICashRegistrationService
    {
        private readonly ApplicationDbContext _context;
        private readonly OperationSettings _settings;

        public CashRegistrationService(ApplicationDbContext context, IOptions<OperationSettings> options)
        {
            _context = context;
            _settings = options.Value;
        }

        public bool HasExistingOpenRegistration()
        {
            var now = DateTime.Now;
            var startHour = _settings.DayStartHour;
            if (now.TimeOfDay < TimeSpan.FromHours(startHour))
                now = now.Subtract(TimeSpan.FromDays(1));
            var limitStartDate = now.Date.AddHours(startHour);
            var limitEndDate = limitStartDate.AddDays(1);
            return _context.CashRegistrations.SingleOrDefault(x => x.Moment >= limitStartDate && x.Moment < limitEndDate && x.Type == RegistrationType.Opening) != null;
        }

        public bool HasExistingCloseRegistration()
        {
            var now = DateTime.Now;
            var startHour = _settings.DayStartHour;
            if (now.TimeOfDay < TimeSpan.FromHours(startHour))
                now = now.Subtract(TimeSpan.FromDays(1));
            var limitStartDate = now.Date.AddHours(startHour);
            var limitEndDate = now.Date.AddDays(1);
            return _context.CashRegistrations.SingleOrDefault(x => x.Moment >= limitStartDate && x.Moment < limitEndDate && x.Type == RegistrationType.Closing) != null;
        }

        public Tuple<decimal?, decimal?> GetCashRegistrations(DateTime day)
        {
            var startHour = _settings.DayStartHour;
            var startDateTime = day.Date.AddHours(startHour);
            var endDateTime = startDateTime.AddDays(1);
            var registrations = _context.CashRegistrations.Where(x => x.Moment >= startDateTime && x.Moment < endDateTime).ToList();
            var open = registrations.FirstOrDefault(x => x.Type == RegistrationType.Opening);
            var close = registrations.FirstOrDefault(x => x.Type == RegistrationType.Closing);
            return new Tuple<decimal?, decimal?>(open?.Cash, close?.Cash);
        }
    }
}
