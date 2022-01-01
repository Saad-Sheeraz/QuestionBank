namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCategory")]
    public partial class tblCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCategory()
        {
            tblQuestions = new HashSet<tblQuestion>();
        }

        [Key]
        public int categoryID { get; set; }

        public int? classId { get; set; }

        public int? subjectId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="please enter name!")]
        public string categoryName { get; set; }

        [StringLength(50)]
        public string createdBy { get; set; }

        public DateTime? createdOn { get; set; }

        [StringLength(50)]
        public string updatedBy { get; set; }

        public DateTime? updatedOn { get; set; }

        public bool? status { get; set; }

        public virtual tblDegree tblDegree { get; set; }

        public virtual tblSubject tblSubject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblQuestion> tblQuestions { get; set; }
    }
}
