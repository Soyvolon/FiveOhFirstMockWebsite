using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data
{
    public class TrooperFlag
    {
        [Key]
        public int FlagId { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedOn { get; set; }
    }
}
