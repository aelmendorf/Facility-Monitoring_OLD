namespace FacilityMonitoring.Common.Data.Entities {
    public partial class GeneratorAlert {
        public int Id { get; set; }
        public int GeneratorId { get; set; }
        public H2GenReading H2GenReading { get; set; }

        public GenericAlert Pressure { get; set; }
    }
}
