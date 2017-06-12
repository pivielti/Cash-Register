using System;
using System.Collections.Generic;
using CashRegister.Web.Models;
using CashRegister.Web.Models.DbContext;

namespace CashRegister.Web.Services
{
    public interface IOperationService
    {
        IEnumerable<OperationGroupModel> GetOperationsByDay(DateTime startDay, DateTime endDay);

        Operation CreateOrUpdate(Operation operation);

        IEnumerable<Operation> GetDailyOperations();
    }
}
