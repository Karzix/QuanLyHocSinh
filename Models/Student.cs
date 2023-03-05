using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        [RegularExpression(@"^[0-9]+[0-9\s]*$")]
        [Required]
        [Display(Name= "Mã số sinh viên")]
        public string Mssv { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc? LopHoc { get; set; }
        [ForeignKey("LopHoc")]
        public Guid? LopHocId { get; set; }

    }
}
