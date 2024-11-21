namespace Version2.Models
{
    public class SystemStateDTO
    {
        public List<HeaterDTO> Heaters { get; set; }
        public List<FanDTO> Fans { get; set; }
    }

    public class HeaterDTO
    {
        public int HeaterId { get; set; }
        public int Level { get; set; } // Heater level (0-5)
    }

    public class FanDTO
    {
        public int FanId { get; set; }
        public bool IsOn { get; set; } // Fan on/off state
    }
}
