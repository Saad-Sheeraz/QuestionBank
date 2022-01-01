namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblRighAnswer
    {
        public int id { get; set; }

        public int? questionId { get; set; }

        public int? answerId { get; set; }

        public int? answerid_C { get; set; }

        public int? answerid_V { get; set; }

        public int? answerid_A { get; set; }

        [StringLength(50)]
        public string reasonText_C { get; set; }

        [StringLength(50)]
        public string reasonText_V { get; set; }

        [StringLength(50)]
        public string reasonText_A { get; set; }
    }
}
