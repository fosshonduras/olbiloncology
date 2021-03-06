﻿namespace OLBIL.OncologyDomain.Entities
{
    public class AdministrativeDivision : BaseEntity
    {
        /// <summary>
        /// The autogenerated Id in the database
        /// </summary>
        public int AdministrativeDivisionId { get; set; }

        public int? ParentId { get; set; }

        /// <summary>
        /// A human-friendly code to identify this entity
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A native-language name for the adminitrative division
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The hierarchy level of the administrative division where 1 is a country
        /// </summary>
        public int? Level { get; set; }

        public virtual AdministrativeDivision Parent { get; set; }
    }
}
