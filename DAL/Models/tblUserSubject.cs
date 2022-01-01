namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblUserSubject")]
    public partial class tblUserSubject
    {
        public int id { get; set; }

        public int? subjectId { get; set; }

        [StringLength(128)]
        public string userId { get; set; }
    }
}
