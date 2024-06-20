using System.ComponentModel.DataAnnotations;

namespace Parser.Models
{
    public class RequestedBodyData
    {
        [Required(ErrorMessage = "Не передан id заказа")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "id заказа не должен быть пустым/превышать 20 символов")]
        [RegularExpression(@"[0-9]{1,20}", ErrorMessage ="Нельзя вводить ничего кроме цифр")]
        public string PurchaseId { get; set; }
    }
}
