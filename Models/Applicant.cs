using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using resumemanager.Models;

namespace resumemanager.obj
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo es requerido")]
        [StringLength(150)]
        [DisplayName("Nombre")]
        public string Name { get; set; } = ""; 
        [Required(ErrorMessage ="El campo es requerido")]
        [StringLength(10)]
        [DisplayName("Genero")]
        public string Gender { get; set; } = "";
        [Required]
        [Range(25,55,ErrorMessage ="Actualmente, no tenemos puestos vacantes para tu edad.")]
        [DisplayName("Edad")]
        public int Age { get; set; }
        [Required(ErrorMessage ="El campo es requerido")]
        [StringLength(50)]      
        [Display(Name = "Clasificación")]          
        public string Qualification { get; set; } = "";
        [Required]
        [Range(1,25,ErrorMessage ="Actualmente, no tenemos puestos vacantes para su experiencia.")]
        [DisplayName("Experiencia en programación")]
        public int TotalExperiencie { get; set; }

        public virtual List<Experience> Experiencies { get; set; } = new List<Experience>();

        public string PhotoUrl { get; set; }
        [Required(ErrorMessage ="Elija la foto de perfil")]
        [DisplayName("Foto de perfil")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }

    }
}