using System;
using System.Collections.Generic;
using System.Text;

namespace OLBIL.OncologyDomain.Entities
{
    public class MedicalExamFile : BaseEntity
    {
        public int MedicalExamFileId { get; set; }
        public string Filename { get; set; }
        public string Location { get; set; }
    }
}
