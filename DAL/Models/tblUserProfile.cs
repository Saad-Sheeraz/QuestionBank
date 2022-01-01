namespace DAL
{
    using DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using System.Data.Entity.Spatial;
    using System.Web;
    [Table("tblUserProfile")]
    public partial class tblUserProfile
    {
        public int id { get; set; }

        [StringLength(150)]
        //[Required(ErrorMessage ="please enter name!")]
        public string userName { get; set; }

        [StringLength(13)]
        //[Required(ErrorMessage ="please enter cnic!")]
        public string cnic { get; set; }
        
        [StringLength(50)]
        //[RegularExpression("^[0-9]{3}-[0-9]{7}$", ErrorMessage = "Mobile No must follow the XXX-XXXXXXX format!")]
        [Required(ErrorMessage = "please enter mobile-no!")]
        public string phone { get; set; }

        [StringLength(50)]
        //[Required(ErrorMessage ="please enter email")]
        public string email { get; set; }

      
        //[Required(ErrorMessage ="please enter address")]
        [StringLength(150)]
        public string address { get; set; }


        [StringLength(50)]
        public string userType { get; set; }


        [StringLength(150)]
        public string profilePic { get; set; }

        [StringLength(150)]
        public string picName { get; set; }

        [StringLength(128)]
        public string userid { get; set; }


        [StringLength(100)]
        public string createdBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createdOn { get; set; }

        [StringLength(100)]
        public string updatedBy { get; set; }

        [StringLength(100)]
        public string updatedOn { get; set; }

        public bool? status { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        //  public virtual tblUserType tblUserType { get; set; }
    }
}
