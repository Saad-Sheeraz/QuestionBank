namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblViewData")]
    public partial class tblViewData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int answerID { get; set; }

        [Column(TypeName = "text")]
        public string answerText1 { get; set; }

        [Column(TypeName = "text")]
        public string answerText2 { get; set; }

        [Column(TypeName = "text")]
        public string answerText3 { get; set; }

        [Column(TypeName = "text")]
        public string answerText4 { get; set; }

        [Column(TypeName = "text")]
        public string answerRight { get; set; }

        [StringLength(50)]
        public string bookName { get; set; }

        [StringLength(50)]
        public string pageNum { get; set; }

        [StringLength(50)]
        public string questionNum { get; set; }

        [StringLength(50)]
        public string paragraphNum { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int categoryID { get; set; }

        [StringLength(50)]
        public string categoryName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int complexityID { get; set; }

        [StringLength(50)]
        public string complexityLevel { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int degreeID { get; set; }

        [StringLength(50)]
        public string degreeName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int questionID { get; set; }

        [Column(TypeName = "text")]
        public string questionText { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int reasonID { get; set; }

        [StringLength(50)]
        public string resaonName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int subjectID { get; set; }

        [StringLength(50)]
        public string subjectName { get; set; }
    }
}
