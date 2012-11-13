using System;
using System.ComponentModel.DataAnnotations;

namespace Task.Metadata
{
    public class GirlMetadata
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "Weight")]
        public double Weight { get; set; }

        [Required]
        [Display(Name = "Height")]
        public double Height { get; set; }
    }
}