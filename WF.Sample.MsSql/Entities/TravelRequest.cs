namespace WF.Sample.MsSql
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
 

    [Table("TravelRequest")]
    public partial class TravelRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TravelRequest()
        {
            State = "Travel Request Created";
            StateName = "Travel Request Created";
        }

        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        [Required]
        [StringLength(256)]
        public string TravelRequestNumber { get; set; }

        public string Comment { get; set; }

        public Guid AuthorId { get; set; }

        public Guid? ManagerId { get; set; }

        public decimal TotalCost { get; set; }

        [Required]
        [StringLength(1024)]
        public string State { get; set; }

        [StringLength(1024)]
        public string StateName { get; set; }

        public virtual Employee Author { get; set; }

        public virtual Employee Manager { get; set; }
    }
}
