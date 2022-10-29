using System.ComponentModel.DataAnnotations;

namespace Store.Api.Models
{
    public class CategoryDtoModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [StringLength(255, ErrorMessage = "{0} không quá {1} ký tự và nhiều hơn {2} ký tự", MinimumLength = 3)]
        public string? CategoryName { get; set; }
    }
}
