using PudelkoL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PudelkoL
{
    public static class UnitUtility
    {
        public static double FromMeter(double number)
        {
            //return (Math.Round(number, 3) * 1000); //OG
            return (Math.Floor(number * 1000));
        }
        public static double FromCentimeter(double number)
        {
            //return (Math.Round(number, 1) * 10); //OG
            return (Math.Floor(number * 10));
        }
        public static double FromMilimeter(double number)
        {
            return Math.Floor(number);
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
        public static double[] ThreeFromMilimeter(double a, double b, double c)
        {
            double[] numbers = { FromMilimeter(a), FromMilimeter(b), FromMilimeter(c) };
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
        
        
        public static double OneToAny(double a, UnitOfMeasure desired)
        {
            if(desired == UnitOfMeasure.meter)
                return ToMeter(a);
            if(desired == UnitOfMeasure.centimeter)
                return ToCentimeter(a);
            return a;
        }


        public static double[] ThreeFromAny(double a, double b, double c, UnitOfMeasure current)
        {
            if(current == UnitOfMeasure.meter)
                return ThreeFromMeter(a, b, c);
            if(current == UnitOfMeasure.centimeter)
                return ThreeFromCentimeter(a, b, c);
            return ThreeFromMilimeter(a, b, c);
        }
        public static double[] ThreeToAny(double a, double b, double c, UnitOfMeasure desired)
        {
            if(desired == UnitOfMeasure.meter)
                return new double[] {ToMeter(a), ToMeter(b), ToMeter(c)};
            if (desired == UnitOfMeasure.centimeter)
                return new double[] { ToCentimeter(a), ToCentimeter(b), ToCentimeter(c) };
            return new double[] { a, b, c };
        }

        public static double[] ThreeFromAnyToAny(double a, double b, double c, UnitOfMeasure current, UnitOfMeasure desired)
        {
            double[] converted = ThreeFromAny(a, b, c, current);
            return ThreeToAny(converted[0], converted[1], converted[2], desired);
        }
    }
}
