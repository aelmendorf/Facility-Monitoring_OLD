namespace FacilityMonitoring.Common.Data.Entities {
    public partial class TankScaleAlert {
        public int Id { get; set; }
        public int AmmoniaControllerReadingId { get; set; }
        public TankScaleReading AmmoniaControllerReading { get; set; }

        public GenericAlert Tank1Alert { get; set; }
        public GenericAlert Tank2Alert { get; set; }
        public GenericAlert Tank3Alert { get; set; }
        public GenericAlert Tank4Alert { get; set; }
    }
}
