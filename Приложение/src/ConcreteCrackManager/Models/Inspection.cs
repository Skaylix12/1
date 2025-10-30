
using System;
using System.Collections.Generic;

namespace ConcreteCrackManager.Models
{
    public class Inspection
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public DateTime InspectedAt { get; set; }
        public int? InspectorId { get; set; }
        public AppUser? Inspector { get; set; }

        public List<ImageEntity> Images { get; set; } = new();
    }
}
