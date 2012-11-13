using System;

namespace Task.BLLModels
{
    public class NewsView
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }
    }
}
