using System;
using System.Collections.Generic;
using System.Text;

namespace Pudelko
{
    public static class UnitUtility
    {
        public static double FromMeter(double number)
        {
            return (Math.Round(number, 3) * 1000);
        }
        public static double FromCentimeter(double number)
        {
            return (Math.Round(number, 1) * 10);
        }
        public static double FromMilimeter(double number)
        {
            return Math.Round(number, 0);
        }
        public static double[] ThreeFromMeter(double a, double b, double c)
        {
            double[] numbers =  { FromMeter(a), FromMeter(b), FromMeter(c) };
            return numbers;
        }
        public static double[] ThreeFromCentimeter(double a, double b, double c)
        {
            double[] numbers =  { FromCentimeter(a), FromCentimeter(b), FromCentimeter(c) };
            return numbers;
        }


        public static double ToMeter(double number)
        {
            return (Math.Round((number / 1000), 3));
        }
        public static double ToCentimeter(double number)
        {
            return (Math.Round((number / 10), 1));
        }
        

    }
}
