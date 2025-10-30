
using System;
using System.Collections.Generic;

namespace ConcreteCrackManager.Models
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public int InspectionId { get; set; }
        public Inspection? Inspection { get; set; }
        public string FilePath { get; set; } = null!;
        public DateTime UploadedAt { get; set; }

        public List<Defect> Defects { get; set; } = new();
    }
}
