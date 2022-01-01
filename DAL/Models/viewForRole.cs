namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class viewForRole
    {
        [Key]
        [Column(Order = 0)]
        public string Id { get; set; }

        public string RollId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubId { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(256)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(256)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 4)]
        public string RoleId { get; set; }

        public int? subjectId { get; set; }

        [StringLength(150)]
        public string nameProfile { get; set; }

        [StringLength(13)]
        public string cnic { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [StringLength(50)]
        public string emailProfile { get; set; }

        [StringLength(50)]
        public string userType { get; set; }

        [StringLength(150)]
        public string address { get; set; }

        [StringLength(150)]
        public string profilePic { get; set; }

        [StringLength(150)]
        public string picName { get; set; }

        [StringLength(128)]
        public string userid { get; set; }
    }
}
