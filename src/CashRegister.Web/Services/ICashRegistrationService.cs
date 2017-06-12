using System;

namespace CashRegister.Web.Services
{
    public interface ICashRegistrationService
    {
        bool HasExistingOpenRegistration();

        bool HasExistingCloseRegistration();

        Tuple<decimal?, decimal?> GetCashRegistrations(DateTime day);
    }
}
