using System.ComponentModel.DataAnnotations;

namespace TransactionWebAPI.Models.Dto
{
    public class TransactionCreateDTO
    {

        public int CategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        public decimal Value { get; set; }


    }
}
