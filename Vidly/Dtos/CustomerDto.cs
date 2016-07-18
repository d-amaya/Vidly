using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;
using Vidly.Models.CustomValidations;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSuscribedToNewsletter { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Membresia es requerido.")]
        public byte MembershipTypeId { get; set; }
    }
}