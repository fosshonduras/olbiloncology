﻿using System.Collections.Generic;

namespace OLBIL.OncologyCore.Entities
{
    public class HospitalUnit : BaseEntity
    {
        public HospitalUnit()
        {
            Wards = new HashSet<Ward>();
        }
        public int UnitId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ward> Wards { get; set; }
    }
}