namespace configurator_shop.Interfaces
{
    public interface IValueDimensionApplier
    {
        public string TechNm(int? value);
        
        public string LenMm(int? value);
        
        public string Item(int? value);
        
        public string EnerW(int? value);
        
        public string FreqMHz(int? value);
        
        public string FreqGHz(int? value);
        
        public string SpeedSpinsPreMinute(int? value);
        
        public string NoiseDb(int? value);
        
        public string WeightGr(int? value);
        
        public string MemoryTb(int? value);
        
        public string MemoryGb(int? value);
        
        public string MemoryGbPerSecond(int? value);
        
        public string MemoryMb(int? value);
        
        public string MemoryMbPerSecond(int? value);
        
        public string MemoryKb(int? value);
        
        public string MemoryByte(int? value);

        public string MemoryBit(int? value);
    }
}