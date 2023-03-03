using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Mssv { get; set; }
        public string Name { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc? LopHoc { get; set; }
        [ForeignKey("LopHoc")]
        public Guid? LopHocId { get; set; }

    }
}
