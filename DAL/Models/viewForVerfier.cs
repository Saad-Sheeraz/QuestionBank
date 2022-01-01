namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("viewForVerfier")]
    public partial class viewForVerfier
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int answerID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int rightID { get; set; }

        [Column(TypeName = "text")]
        public string answerText1 { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int categoryID { get; set; }

        [StringLength(50)]
        public string categoryName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int subjectID { get; set; }

        [StringLength(50)]
        public string subjectName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int degreeID { get; set; }

        [StringLength(50)]
        public string degreeName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int questionID { get; set; }

        [Column(TypeName = "text")]
        public string questionText { get; set; }

        [StringLength(50)]
        public string bookPageNum { get; set; }

        [StringLength(50)]
        public string updatedBy { get; set; }

        public DateTime? updatedOn { get; set; }

        [StringLength(50)]
        public string verifiedBy { get; set; }

        public DateTime? verifiedDate { get; set; }

        [StringLength(50)]
        public string approvedBy { get; set; }

        public DateTime? approvedDate { get; set; }

        [StringLength(50)]
        public string statusInfo { get; set; }

        [StringLength(50)]
        public string reasonText_C { get; set; }

        [StringLength(50)]
        public string reasonText_V { get; set; }

        [StringLength(50)]
        public string reasonText_A { get; set; }

        public int? answerid_A { get; set; }

        public int? answerid_V { get; set; }

        public DateTime? createdOn { get; set; }

        [StringLength(250)]
        public string createdBy { get; set; }

        [StringLength(150)]
        public string questionPic { get; set; }

        public bool? picBool { get; set; }

        [StringLength(150)]
        public string answerPic { get; set; }

        public bool? ansPicBool { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }
    }
}
