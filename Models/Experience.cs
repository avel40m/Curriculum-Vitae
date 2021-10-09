using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using resumemanager.obj;

namespace resumemanager.Models
{
    public class Experience
    {
        public Experience()
        {
        }
        [Key]
        public int ExperienceId { get; set; }
        [ForeignKey("Applicant")]
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant{ get; private set; }
        [Display(Name ="Nombre de la compañia")]
        public string CompanyName { get; set; }
        [Display(Name = "Designación")]
        public string Designation { get; set; }
        [Display(Name = "Años trabajados")]
        [Range(1,25,ErrorMessage = "Los años deben estar entre 1 y 25")]
        [Required]
        public int YearsWorked { get; set; }
    }
}