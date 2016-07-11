using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models.CustomValidations;

namespace Vidly.Models
{
    public class Customer
    {
        public static readonly int UNKNOWN = 0;
        public static readonly int PAY_AS_YOU_GO = 1;

        public int Id { get; set; }

        [Display(Name = "Date of bitrth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSuscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Membresia es requerido.")]
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
    }
}