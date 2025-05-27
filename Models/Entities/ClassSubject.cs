using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class ClassSubject
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(3,3)]
        public int Credits { get; set; }
    }
}
