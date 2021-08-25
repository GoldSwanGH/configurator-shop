using configurator_shop.Interfaces;

namespace configurator_shop.Services
{
    public class ValueDimensionApplier : IValueDimensionApplier
    {
        public string TechNm(int? value)
        {
            return value != null ? "" + value + " нм" : null;
        }

        public string LenMm(int? value)
        {
            return value != null ? "" + value + " мм" : null;
        }

        public string Item(int? value)
        {
            return value != null ? "" + value + " шт" : null;
        }

        public string EnerW(int? value)
        {
            return value != null ? "" + value + " Вт" : null;
        }

        public string FreqMHz(int? value)
        {
            return value != null ? "" + value + " МГц" : null;
        }

        public string FreqGHz(int? value)
        {
            if (value != null)
            {
                double ghz = (double)value / 1000;
                return "" + ghz + " ГГц";
            }
            else
            {
                return null;
            }
        }

        public string SpeedSpinsPreMinute(int? value)
        {
            return value != null ? "" + value + " об/мин" : null;
        }

        public string NoiseDb(int? value)
        {
            return value != null ? "" + value + " дБ" : null;
        }

        public string WeightGr(int? value)
        {
            return value != null ? "" + value + " г" : null;
        }

        public string MemoryTb(int? value)
        {
            if (value != null)
            {
                double gb = (double)value / 1024000 ;
                return "" + gb + " Тб";
            }
            else
            {
                return null;
            }
        }

        public string MemoryGb(int? value)
        {
            if (value != null)
            {
                double gb = (double)value / 1024;
                return "" + gb + " Гб";
            }
            else
            {
                return null;
            }
        }

        public string MemoryGbPerSecond(int? value)
        {
            return  value != null ? MemoryGb(value) + "/с" : null;
        }

        public string MemoryMb(int? value)
        {
            return value != null ? "" + value + " Мб" : null;
        }

        public string MemoryMbPerSecond(int? value)
        {
            return value != null ? MemoryMb(value) + "/с" : null;
        }

        public string MemoryKb(int? value)
        {
            return value != null ? "" + value + " Кб" : null;
        }

        public string MemoryByte(int? value)
        {
            return value != null ? "" + value + " Б" : null;
        }

        public string MemoryBit(int? value)
        {
            return value != null ? "" + value + "-bit" : null;
        }
    }
}