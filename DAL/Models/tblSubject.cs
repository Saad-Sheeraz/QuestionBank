namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblSubject")]
    public partial class tblSubject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSubject()
        {
            tblCategories = new HashSet<tblCategory>();
            tblQuestions = new HashSet<tblQuestion>();
            tblBooks = new HashSet<tblBook>();
        }

        [Key]
        public int subjectID { get; set; }

        //[Required(ErrorMessage ="please select degree!")]
        public int? classId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="please enter subject!")]
        public string subjectName { get; set; }

        [StringLength(50)]
        public string createdBy { get; set; }

        public DateTime? createdOn { get; set; }

        [StringLength(50)]
        public string updatedBy { get; set; }

        public DateTime? updatedOn { get; set; }

        public bool? status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCategory> tblCategories { get; set; }

        public virtual tblDegree tblDegree { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblQuestion> tblQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBook> tblBooks { get; set; }
    }
}
