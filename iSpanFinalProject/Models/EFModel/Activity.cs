namespace iSpanFinalProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activity()
        {
            ActivityFollows = new HashSet<ActivityFollow>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string activityName { get; set; }

        public DateTime activityStartTime { get; set; }

        public DateTime activityEndTime { get; set; }

        [Required]
        [StringLength(100)]
        public string activityLocation { get; set; }

        public int activityTypeId { get; set; }

        [Required]
        [StringLength(4000)]
        public string activityInfo { get; set; }

        public int activityOrganizerId { get; set; }

        [Required]
        [StringLength(50)]
        public string activityImagePath { get; set; }

        public bool publishedStatus { get; set; }

        public int checkedById { get; set; }

        public virtual ActivityType ActivityType { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityFollow> ActivityFollows { get; set; }
    }
}
