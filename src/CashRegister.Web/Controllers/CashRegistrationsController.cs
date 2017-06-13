using System;
using System.Linq;
using System.Threading.Tasks;
using CashRegister.Web.DataAccess;
using CashRegister.Web.Models.DbContext;
using CashRegister.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/cash-registrations")]
    public class CashRegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICashRegistrationService _service;

        public CashRegistrationsController(ApplicationDbContext context, ICashRegistrationService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var canOpen = !_service.HasExistingOpenRegistration();
            var canClose = !canOpen && !_service.HasExistingCloseRegistration();
            var records = await _context.CashRegistrations.OrderByDescending(x => x.Moment).ToListAsync();
            return Ok(new {
                canOpen = canOpen,
                canClose = canClose,
                registrations = records
            });
        }

        [HttpGet("hasopened")]
        public IActionResult HasOpened()
        {
            var hasOpened = _service.HasExistingOpenRegistration();
            return Ok(hasOpened);
        }

        [HttpGet("hasclosed")]
        public IActionResult HasClosed()
        {
            var hasOpened = _service.HasExistingOpenRegistration();
            return Ok(hasOpened);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCashRegistration([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _context.CashRegistrations.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Open([FromBody, Bind(nameof(CashRegistration.Cash))] CashRegistration model)
        {
            if (!_service.HasExistingOpenRegistration())
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.Type = RegistrationType.Opening;
            model.Moment = DateTime.Now;

            _context.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCashRegistration), new {
                id = model.Id
            }, model);
        }

        [HttpPost]
        public async Task<IActionResult> Close([FromBody, Bind(nameof(CashRegistration.Cash))] CashRegistration model)
        {
            if (!_service.HasExistingOpenRegistration())
                return Unauthorized();
            if (_service.HasExistingCloseRegistration())
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(model);

            model.Type = RegistrationType.Closing;
            model.Moment = DateTime.Now;

            _context.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCashRegistration), new {
                id = model.Id
            }, model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cashRegistration = await _context.CashRegistrations.SingleOrDefaultAsync(m => m.Id == id);
            if (cashRegistration == null)
                return NotFound();

            _context.CashRegistrations.Remove(cashRegistration);
            await _context.SaveChangesAsync();

            return Ok(cashRegistration);
        }
    }
}