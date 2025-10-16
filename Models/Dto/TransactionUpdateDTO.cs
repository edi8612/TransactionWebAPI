using System.ComponentModel.DataAnnotations;

namespace TransactionWebAPI.Models.Dto
{
    public class TransactionUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
        public int CategoryId { get; set; }

    }
}
