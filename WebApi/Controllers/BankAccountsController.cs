using LocalDBWebApiUsingEF.Data;
using LocalDBWebApiUsingEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalDBWebApiUsingEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly DBManager _context;

        public BankAccountsController(DBManager context)
        {
            _context = context;
        }

        // GET: api/BankAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetAllBankAccounts()
        {
            return await _context.BankAccounts
                    .Include(b => b.User)
                    .Include(b => b.Transactions)
                    .ToListAsync();
        }

        // POST: api/BankAccounts
        [HttpPost]
        public async Task<ActionResult<BankAccount>> CreateBankAccount(BankAccount account)
        {
            if (!UsersController.ValidUsername(account.UserUsername))
            {
                return BadRequest("Invalid bank account user details entered.");
            }
            try
            {
                _context.BankAccounts.Add(account);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBankAccount), new { id = account.AccountNumber }, account);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Bank account already exists with this bank account number.");
            }
        }

        // GET: api/BankAccounts/12345
        [HttpGet("{accountNumber}")]
        public async Task<ActionResult<BankAccount>> GetBankAccount(int accountNumber)
        {
            var account = await _context.BankAccounts
                            .Include(b => b.User)
                            .Include(b => b.Transactions)
                            .FirstOrDefaultAsync(b => b.AccountNumber == accountNumber);

            if (account == null)
            {
                return NotFound("Bank account not found.");
            }
            return account;
        }

        // PUT: api/BankAccounts/12345
        [HttpPut("{accountNumber}")]
        public async Task<IActionResult> UpdateBankAccount(int accountNumber, BankAccount account)
        {
            if (accountNumber != account.AccountNumber)
            {
                return BadRequest("Bank account numbers do not match.");
            }
            if (!UsersController.ValidUsername(account.UserUsername))
            {
                return BadRequest("Invalid bank account user details entered.");
            }
            _context.Entry(account).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BankAccounts.Any(e => e.AccountNumber == accountNumber))
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

        // DELETE: api/BankAccounts/12345
        [HttpDelete("{accountNumber}")]
        public async Task<IActionResult> DeleteBankAccount(int accountNumber)
        {
            var account = await _context.BankAccounts.FindAsync(accountNumber);
            if (account == null)
            {
                return NotFound("Bank account not found.");
            }
            _context.BankAccounts.Remove(account);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
