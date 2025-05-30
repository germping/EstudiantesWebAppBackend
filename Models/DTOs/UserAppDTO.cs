using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UserAppDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public String Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "El password debe tener entre 4 y 10 caracteres")]
        public string Password { get; set; }
        [Required(ErrorMessage = "IdProfile is required")]
        public int idProfile { get; set; }
        public string email { get; set; }
    }
}
