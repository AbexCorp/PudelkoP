using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Pudelko.Enums;


namespace Pudelko
{
    //Constructor
    public sealed class Pudelko : IEquatable<Pudelko> //: IFormattable #FIX#
    {
        //To do
        //IFormatable
        //8 Operator +
        //9
        //10
        //11
        //12
        //13
        //14
        //15
        //16


        //2,546m = 254,6cm = 2546mm
        //max size = 10m x 10m x 10m
        //unmutable
        //default is 10 x 10 x 10 cm
        //sealed (can't inherit)

        // #FIX# unitOfMeasure of every Pudelko is always set to milimeter
        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {

            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentOutOfRangeException("Each side of the box must be at least 1mm wide");


            //Store size as milimeters
            double[] dimensions = UnitUtility.ThreeFromAnyToAny(a, b, c, unit, UnitOfMeasure.milimeter);

            if (dimensions[0] > 10000 || dimensions[1] > 10000 || dimensions[2] > 10000)
                throw new ArgumentOutOfRangeException("Any side of the box can't be longer than 10m");

            this.a = dimensions[0];
            this.b = dimensions[1];
            this.c = dimensions[2];

        }


        //Variables
        private double a;
        private double b;
        private double c;
        private UnitOfMeasure unitOfMeasure;


        //Properties
        public double A
        { get { return UnitUtility.ToMeter(a); } }
        public double B
        { get { return UnitUtility.ToMeter(b); } }
        public double C
        { get { return UnitUtility.ToMeter(c); } }
        public UnitOfMeasure UnitOfMeasure { get { return unitOfMeasure; } }

        public double Objetosc
        {
            get { return Math.Round((A * B * C), 9); }
        }
        public double Pole
        {
            get { return Math.Round((2 * (A * B) + 2 * (B * C) + 2 * (C * A)), 6); }
        }


        //To String
        //ToString(string format, IFormatProvider? formatProvider) { } //#FIX#
        override public string ToString()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            return $"{UnitUtility.ToMeter(a)} m × {UnitUtility.ToMeter(b)} m × {UnitUtility.ToMeter(c)} m";
        }

        public string ToString(string format = "m")
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            switch (format)
            {
                case "m":
                    return $"{UnitUtility.ToMeter(a)} m × {UnitUtility.ToMeter(b)} m × {UnitUtility.ToMeter(c)} m";

                case "cm":
                    return $"{UnitUtility.ToCentimeter(a)} cm × {UnitUtility.ToCentimeter(b)} cm × {UnitUtility.ToCentimeter(c)} cm";

                case "mm":
                    return $"{a} mm × {b} mm × {c} mm";

                default:
                    throw new FormatException("This format is not supported");
            }
        }

        //Equatable
        public override bool Equals(object obj)
        {
            if (obj is Pudelko)
                return Equals((Pudelko)obj);

            return base.Equals(obj);
        }
        public bool Equals(Pudelko box)
        {
            //return Equals(box, UnitOfMeasure.milimeter);
            if (box == null || GetType() != box.GetType())
            {
                return false;
            }
            // TODO: write your implementation of Equals() here

            Console.WriteLine();
            Console.WriteLine(box.unitOfMeasure);
            Console.WriteLine(box.a);
            Console.WriteLine(box.b);
            Console.WriteLine(box.c);

            double[] temp = UnitUtility.ThreeFromAnyToAny(box.A, box.B, box.C, UnitOfMeasure.meter, UnitOfMeasure.milimeter);
            Console.WriteLine(UnitOfMeasure.milimeter);
            Console.WriteLine(temp[0]);
            Console.WriteLine(temp[1]);
            Console.WriteLine(temp[2]);


            return false;
        }


        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return A.GetHashCode() + B.GetHashCode() + C.GetHashCode() + unitOfMeasure.GetHashCode();
            //return base.GetHashCode();
        }



        /* #FIX# == throws exception when Equals() is called???
        public static bool operator ==(Pudelko boxA, Pudelko boxB)
        {
            return boxA.Equals(boxB);
        }
        public static bool operator !=(Pudelko boxA, Pudelko boxB)
        {
            return !boxA.Equals(boxB);
        }
        */
    }
}
