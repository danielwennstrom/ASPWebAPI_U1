using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public int AssignedUserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Status { get; set; }
    }
}
