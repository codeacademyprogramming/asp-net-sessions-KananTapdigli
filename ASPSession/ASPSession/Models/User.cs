using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPSession.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(3,ErrorMessage ="Name field must be at least 3 character"),MaxLength(15)]
        public string Username { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(30)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage ="Correct Format : example@gmail.com")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password field must be at least 8 character")]
        public string Password { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Confirm Password field must be at least 8 character")]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Confirm Password does not Match Password")]
        public string confirmPassword { get; set; }
    }
}