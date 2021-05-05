using FiveOhFirstMock.Data.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Forms
{
    public class BatchQualificationSubmission
    {
        public List<Trooper> Members { get; set; } = new();
        public List<Trooper> Instructors { get; set; } = new();
        public Qualifications Qualification { get; set; }
        public ConcurrentDictionary<int, QualificationSubmission> Submissions { get; set; } = new();
    }

    public class QualificationSubmission
    {
        [Key]
        public int SubmissionKey { get; set; }
        public Qualifications Qualifications { get; set; }
        public QualificationStatus Status { get; set; }
        public string Notes { get; set; } = "";

        public int TrooperId { get; set; }
        public Trooper Trooper { get; set; }

        public List<Trooper> Instructors { get; set; } = new();
    }
}
