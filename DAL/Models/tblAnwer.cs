namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblAnwer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblAnwer()
        {
        //E:\working here\QustionProjectCTSP\DAL\Models\tblAnwer.cs
            tblRighAnswers = new HashSet<tblRighAnswer>();
        }
        [Key]
        public int answerID { get; set; }

        public int? questionId { get; set; }

        [Column(TypeName = "text")]
        public string answerText1 { get; set; }

        //[Column(TypeName = "text")]
        //public string reasonCreator { get; set; }

        //[Column(TypeName = "text")]
        //public string reasonVerifier { get; set; }

        //[Column(TypeName = "text")]
        //public string reasonApprover { get; set; }

        [Column(TypeName = "text")]
        public string answerRight { get; set; }

        [StringLength(50)]
        public string createdBy { get; set; }

        public DateTime? createdOn { get; set; }

        [StringLength(50)]
        public string updatedBy { get; set; }

        public DateTime? updatedOn { get; set; }

        public bool? status { get; set; }

        [StringLength(150)]
        public string answerPic { get; set; }

        public bool? ansPicBool { get; set; }

        public virtual tblQuestion tblQuestion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRighAnswer> tblRighAnswers { get; set; }
    }
}
