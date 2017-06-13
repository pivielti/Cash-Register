using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CashRegister.Web.Models;
using CashRegister.Web.Models.DbContext;

namespace CashRegister.Web.Services
{
    public interface IOperationService
    {
        IEnumerable<OperationGroupModel> GetOperationsByDay(DateTime startDay, DateTime endDay);

        Task<Operation> CreateOrUpdate(Operation operation);

        IEnumerable<Operation> GetDailyOperations();
    }
}
