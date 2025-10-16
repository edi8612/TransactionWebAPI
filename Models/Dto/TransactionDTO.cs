using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionWebAPI.Models.Dto
{
    public class TransactionDTO
    {
        [Required]
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }

        [Required]
        public string Title { get; set; }
        public decimal Value { get; set; }

      
    }
}
