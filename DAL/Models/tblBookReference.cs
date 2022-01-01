namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblBookReference")]
    public partial class tblBookReference
    {
        [Key]
        public int bookRefID { get; set; }

        [StringLength(50)]
        public string bookName { get; set; }

        [StringLength(50)]
        public string pageNum { get; set; }

        [StringLength(50)]
        public string questionNum { get; set; }

        [StringLength(50)]
        public string paragraphNum { get; set; }

        [StringLength(50)]
        public string createdBy { get; set; }

        public int? createdOn { get; set; }

        [StringLength(50)]
        public string updatedBy { get; set; }

        public int? updatedOn { get; set; }

        public bool? status { get; set; }

        public int? questionId { get; set; }

        public virtual tblQuestion tblQuestion { get; set; }
    }
}
