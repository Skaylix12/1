
namespace ConcreteCrackManager.Models
{
    public class Defect
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public ImageEntity? Image { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Severity { get; set; }
        public string? Description { get; set; }
    }
}
