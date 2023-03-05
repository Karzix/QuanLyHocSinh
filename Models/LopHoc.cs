using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class LopHoc
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Mã lớp học")]
        public string MaLopHoc { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        [Display(Name ="Tên lớp")]
        public string Name { get; set; }
    }
}
