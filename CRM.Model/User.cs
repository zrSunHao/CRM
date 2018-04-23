using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Model
{
    public class User 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string PictureUrl { get; set; }

    }
}
