namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblBook
    {
        public tblBook()
        {
            tblQuestions = new HashSet<tblQuestion>();
        }

        [Key]
        public int bookID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="please enter book name!")]
        public string bookName { get; set; }

        public int? degreeId { get; set; }

        public int? subjectId { get; set; }


        [StringLength(150)]
        public string createdBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createdOn { get; set; }

        [StringLength(150)]
        public string updatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updatedOn { get; set; }

        public bool? status { get; set; }

        public virtual tblDegree tblDegree { get; set; }

        public virtual tblSubject tblSubject { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "please enter author name!")]
        public string bookAuthor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblQuestion> tblQuestions { get; set; }
    }
}
