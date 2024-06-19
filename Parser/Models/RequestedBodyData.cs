using System.ComponentModel.DataAnnotations;

namespace Parser.Models
{
    public class RequestedBodyData
    {
        [Required]
        [RegularExpression(@"[0-9]{1,20}", ErrorMessage = "Некорректный id Покупки")]
        public string PurchaseId { get; set; }
    }
}
