using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class RelationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassSubjectId { get; set; }
        public int IdProfile { get; set; }
        public User user { get; set; }
        public ClassSubject classSubject { get; set; }
    }
}
