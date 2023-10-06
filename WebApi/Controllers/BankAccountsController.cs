using LocalDBWebApiUsingEF.Data;
using LocalDBWebApiUsingEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

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
        // POST: api/BankAccounts/transfer
        //      Sample input
        //      "FromAccountNumber": 10001,
        //      "ToAccountNumber": 10002,
        //      "Amount": 100.00
        [HttpPost("transfer")]
        public async Task<IActionResult> TransferMoney([FromBody] TransferDto transfer)
        {
            // Step 1: Validate the TransferDto
            if (transfer.Amount <= 0)
            {
                return BadRequest("Transfer amount must be greater than zero.");
            }

            var fromAccount = await _context.BankAccounts.FindAsync(transfer.FromAccountNumber);
            var toAccount = await _context.BankAccounts.FindAsync(transfer.ToAccountNumber);

            if (fromAccount == null || toAccount == null)
            {
                return NotFound("Account not found.");
            }

            if (fromAccount.Balance < (double)transfer.Amount)
            {
                return BadRequest("Insufficient funds.");
            }

            // Step 2: Perform the Transfer
            fromAccount.Balance -= (double)transfer.Amount;
            toAccount.Balance += (double)transfer.Amount;

            // Step 3: Create a Transaction record
            var transaction = new Transaction
            {
                FromAccountNumber = transfer.FromAccountNumber,
                ToAccountNumber = transfer.ToAccountNumber,
                Amount = (double)transfer.Amount,
                Description = transfer.Description,
                Timestamp = DateTime.UtcNow // Using UtcNow to avoid timezone issues
            };
            _context.Transactions.Add(transaction);

            // Step 4, and 5: Update the BankAccount Balances and User Model, and Persist Changes
            try
            {
                _context.Update(fromAccount);
                _context.Update(toAccount);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle exception (e.g., concurrency conflict)
                throw;
            }

            // Step 6: Return a Response
            return Ok(new { Message = "Transfer successful" });
        }


        [HttpGet("{accountNumber}/transactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(int accountNumber)
        {
            var transactions = await _context.Transactions
                                .Where(t => t.FromAccountNumber == accountNumber || t.ToAccountNumber == accountNumber)
                                .OrderByDescending(t => t.Timestamp)
                                .ToListAsync();

            if (!transactions.Any())
            {
                return NotFound("No transactions found.");
            }
            return transactions;
        }



    }





}
