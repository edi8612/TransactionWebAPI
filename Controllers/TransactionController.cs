using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionWebAPI.Models.Dto;
using TransactionWebAPI.Services;

namespace TransactionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;

        }


        [HttpGet(Name = "GetTransactions")]

        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            var transactions = await _transactionService.GetAllAsync();

            if (transactions == null)
            {
                return NotFound();
            }

            return Ok(transactions);



        }

        [HttpGet("{id}", Name = "GetTransaction")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }


            return Ok(transaction);


        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TransactionDTO>> CreateTransaction(TransactionCreateDTO dto)
        {
            try
            {


                var transactionCreate = await _transactionService.CreateAsync(dto);
                return CreatedAtRoute(
                    "GetTransaction",
                    new { id = transactionCreate.Id },
                    transactionCreate);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionDTO>> UpdateTransaction(int id, TransactionUpdateDTO dto)
        {
            try
            {
                dto.Id = id;
                var transactionUpdated = await _transactionService.UpdateAsync(dto);

                if (transactionUpdated == null)
                {
                    return NotFound();
                }

                return Ok(transactionUpdated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                await _transactionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
