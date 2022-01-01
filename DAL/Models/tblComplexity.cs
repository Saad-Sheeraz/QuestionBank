namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblComplexity")]
    public partial class tblComplexity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblComplexity()
        {
            tblQuestions = new HashSet<tblQuestion>();
        }

        [Key]
        public int complexityID { get; set; }

        [StringLength(50)]
        public string complexityLevel { get; set; }

        [StringLength(50)]
        public string createdBy { get; set; }

        public DateTime? createdOn { get; set; }

        [StringLength(50)]
        public string updatedBy { get; set; }

        public DateTime? updatedOn { get; set; }

        public int? status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblQuestion> tblQuestions { get; set; }
    }
}
