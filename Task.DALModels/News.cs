using System;
using System.ComponentModel.DataAnnotations;
using Task.DALModels.Interfaces;
using Task.Metadata;

namespace Task.DALModels
{
    [MetadataType(typeof(NewsMetadata))]
    public class News : IBaseModel
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string ShortDescription { get; set; }

        public virtual string Description { get; set; }
    }
}
