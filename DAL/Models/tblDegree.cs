namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDegree")]
    public partial class tblDegree
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblDegree()
        {
            tblCategories = new HashSet<tblCategory>();
            tblQuestions = new HashSet<tblQuestion>();
            tblSubjects = new HashSet<tblSubject>();
            tblBooks = new HashSet<tblBook>();
        }

        [Key]
        public int degreeID { get; set; }

        [Required(ErrorMessage ="please enter degree name!")]
        [StringLength(50)]      
        public string degreeName { get; set; }

        [StringLength(50)]
        public string createdBy { get; set; }

        public DateTime? createdOn { get; set; }

        [StringLength(50)]
        public string updatedBy { get; set; }

        public DateTime? updatedOn { get; set; }

        public bool? status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCategory> tblCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblQuestion> tblQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSubject> tblSubjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBook> tblBooks { get; set; }
    }
}
