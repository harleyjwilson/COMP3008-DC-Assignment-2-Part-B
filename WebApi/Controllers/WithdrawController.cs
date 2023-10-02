using LocalDBWebApiUsingEF.Data;
using LocalDBWebApiUsingEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalDBWebApiUsingEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawController : ControllerBase
    {
        private readonly DBManager _context;

        public WithdrawController(DBManager context)
        {
            _context = context;
        }
        // POST: api/deposit
        [HttpPost]
        public async Task<ActionResult<BankAccount>> ProcessWithdrawal(Transaction transact)
        {
            var account = await _context.BankAccounts
                                     .FirstOrDefaultAsync(b => b.AccountNumber == transact.AccountNumber);
            if (account == null)
            {
                return NotFound("Bank account not found.");
            }

            if (transact.Amount == 0.0)
            {
                return BadRequest("Invalid amount - Can't withdraw 0.0");
            }
            else if (transact.Amount > 0.0)
            {
                transact.Amount = -transact.Amount;
            }

            double newBalance = account.Balance + transact.Amount;

            if (newBalance >= 0.0)
            {
                account.Balance = newBalance;
                account.Transactions.Add(transact);
                _context.Entry(account).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.BankAccounts.Any(e => e.AccountNumber == transact.AccountNumber))
                    {
                        return NotFound("Bank account not found.");
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }
            else
            {
                return BadRequest("Insufficient funds.");
            }
        }
    }
}
