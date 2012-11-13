using System;
using System.ComponentModel.DataAnnotations;

namespace Task.Metadata
{
    public class NewsMetadata
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime? Date { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}