using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Extensions
{
    public static class RandomizerExtension
    {
        public static double GetRandomNumber(this Randomizer randomizer, double minimum, double maximum, int Len)   //Len小数点保留位数
        {
            Random random = new Random();
            return Math.Round(random.NextDouble() * (maximum - minimum) + minimum, Len);
        }
    }
    public static class FakerExtension
    {
       
        public static double CustomDouble(this Faker faker, double min = 0.0, double max = 1.0, int decimalPlaces = 2)
        {
            Randomizer randomizer = new Randomizer();
            return randomizer.GetRandomNumber(min, max, decimalPlaces);
        }
    }



}
