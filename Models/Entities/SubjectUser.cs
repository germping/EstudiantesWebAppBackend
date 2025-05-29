using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class SubjectUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassSubjectId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual User User { get; set; }
        public virtual ClassSubject ClassSubject { get; set; }
    }
}
