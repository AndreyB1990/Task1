using System;
using System.ComponentModel.DataAnnotations;
using Task.DALModels.Interfaces;
using Task.Metadata;

namespace Task.DALModels
{
    [MetadataType(typeof(GirlMetadata))]
    public class Girl : IBaseModel
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime BirthDate { get; set; }

        public virtual double Weight { get; set; }

        public virtual double Height { get; set; }
    }
}
