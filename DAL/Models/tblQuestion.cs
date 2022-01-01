namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblQuestion()
        {
            tblAnwers = new HashSet<tblAnwer>();
            tblRighAnswers = new HashSet<tblRighAnswer>();
        }
        [Key]
        public int questionID { get; set; }

        //[NotMapped]
        //public int myQID { get; set; }

        [NotMapped]
        public string reasonCreator { get; set; }

        public int degreeId { get; set; }

        public int subjectId { get; set; }

        public int categoryId { get; set; }

        public int complexityId { get; set; }

        //adding for books
        //public int bookId { get; set; }
        public int? bookId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string questionText { get; set; }

        //for tbl answer
        [NotMapped]
        [Column(TypeName = "text")]
        public string answerText { get; set; }


        //for tbl bookref
        //[NotMapped]
        //[Required(ErrorMessage ="please enter book name")]
        //public string bookname { get; set; }

        //[NotMapped]
        //[Required(ErrorMessage ="please enter page no")]
        //public string pageno { get; set; }

        [StringLength(50)]
        public string bookPageNum { get; set; }

        [StringLength(250)]
        public string createdBy { get; set; }


        public DateTime? createdOn { get; set; }

        [StringLength(50)]
        public string updatedBy { get; set; }

        public DateTime? updatedOn { get; set; }

        public bool? status { get; set; }

        [StringLength(50)]
        public string verifiedBy { get; set; }

        public DateTime? verifiedDate { get; set; }

        [StringLength(50)]
        public string approvedBy { get; set; }

        [StringLength(50)]
        public string statusInfo { get; set; }

        public DateTime? approvedDate { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }

        [StringLength(150)]
        public string questionPic { get; set; }

        public bool? picBool { get; set; }

        [NotMapped]
        public string answerPic
        {
            set; get;

        }
        //new
        public bool? iteratequestions { get; set; }

        //[NotMapped]
        //public int checkit { set; get; }

            [NotMapped]
        public string scheckit { get; set; } = "val1";

        //[NotMapped]
        
        //public int q_id { set; get; }

        public virtual tblCategory tblCategory { get; set; }

        public virtual tblComplexity tblComplexity { get; set; }

        public virtual tblDegree tblDegree { get; set; }

        public virtual tblSubject tblSubject { get; set; }



        //adding new
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBookReference> tblBookReferences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAnwer> tblAnwers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRighAnswer> tblRighAnswers { get; set; }
    }
}
