namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblPaperMaker")]
    public partial class tblPaperMaker
    {
        [Key]
        public int makerID { get; set; }

        [StringLength(50)]
        public string makerName { get; set; }

        [StringLength(50)]
        public string Cnic { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        [StringLength(50)]
        public string subjectCategory { get; set; }

        public int? subjectId { get; set; }

        public virtual tblSubject tblSubject { get; set; }
    }
}
