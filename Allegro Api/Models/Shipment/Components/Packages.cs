namespace Allegro_Api.Shipment.Components
{
    public class Packages
    {
        public string type { get; set; }
        public DimensionValue length { get; set; }
        public DimensionValue width { get; set; }
        public DimensionValue height { get; set; }
        public WeightValue weight { get; set; }
    }
}