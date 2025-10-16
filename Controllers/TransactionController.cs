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
            try
            {
                var transactions = await _transactionService.GetAllAsync();
                if (transactions == null)
                    return NotFound(new { message = "No transactions found." });

                return Ok(transactions);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

        [HttpGet("{id}", Name = "GetTransaction")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int id)
        {
            try
            {
                var transaction = await _transactionService.GetAsync(id);
                if (transaction == null)
                    return NotFound(new { message = $"Transaction with ID {id} not found." });

                return Ok(transaction);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
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
                return BadRequest(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
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
                    return NotFound(new { message = $"Transaction with ID {id} not found." });

                return Ok(transactionUpdated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Transaction with ID {id} not found." });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
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
                return NotFound(new { message = $"Transaction with ID {id} not found." });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }
    }
}
