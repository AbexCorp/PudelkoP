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
        //8Operator +
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
        public Pudelko(double a = 100, double b = 100, double c = 100, UnitOfMeasure unit = UnitOfMeasure.centimeter) //#FIX# Podaje milimetry a jednostka centymetry
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentOutOfRangeException("Each side of the box must be at least 1mm wide");


            //Store size as milimeters
            switch (unit) //Problem with rounding may exist. #PROBLEM#
            {
                case UnitOfMeasure.meter:
                    unitOfMeasure = unit;
                    a = UnitUtility.FromMeter(a);
                    b = UnitUtility.FromMeter(b);
                    c = UnitUtility.FromMeter(c);
                    break;

                case UnitOfMeasure.centimeter:
                    unitOfMeasure = unit;
                    a = UnitUtility.FromCentimeter(a);
                    b = UnitUtility.FromCentimeter(b);
                    c = UnitUtility.FromCentimeter(c);
                    break;

                case UnitOfMeasure.milimeter:
                    unitOfMeasure = unit;
                    break;
            }
            if (a > 10000 || b > 10000 || c > 10000)
                throw new ArgumentOutOfRangeException("Any side of the box can't be longer than 10m");

            this.a = a;
            this.b = b;
            this.c = c;

        }

        //Variables
        private double a;
        private double b;
        private double c;
        private UnitOfMeasure unitOfMeasure = UnitOfMeasure.centimeter; //May not be needed #FIX#

        //Properties
        public double A
        {
            get{ return UnitUtility.ToMeter(a); }
        }
        public double B
        {
            get { return UnitUtility.ToMeter(b); }
        }
        public double C
        {
            get { return UnitUtility.ToMeter(c); }
        }
        public UnitOfMeasure UnitOfMeasure { get { return unitOfMeasure; } } //May not be needed #FIX#

        public double Objetosc
        {
            get { return Math.Round((UnitUtility.ToMeter(a) * UnitUtility.ToMeter(b) * UnitUtility.ToMeter(c)), 9); }
        }
        public double Pole
        {
            get { return Math.Round( ( 2*(A*B) + 2*(B*C) + 2*(C*A) ) , 6); }
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
            if(obj is Pudelko)
                return Equals((Pudelko)obj);

            return base.Equals(obj);
        }
        public bool Equals(Pudelko box)
        {
            return Equals(box, UnitOfMeasure.milimeter);
        }
        public bool Equals(Pudelko box, UnitOfMeasure unit = UnitOfMeasure.milimeter)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (box == null || GetType() != box.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            double[] pudelko = {box.A, box.B, box.C};
            if(unit == UnitOfMeasure.meter)
                pudelko = UnitUtility.ThreeFromMeter(box.A, box.B, box.C);
            if(unit == UnitOfMeasure.centimeter)
                pudelko = UnitUtility.ThreeFromCentimeter(box.A, box.B, box.C);
            
            double[] dimensions = {a,b,c}; 
            Array.Sort(dimensions);
            Array.Sort(pudelko);

            if(dimensions[0] == pudelko[0] && dimensions[1] == pudelko[1] && dimensions[2] == pudelko[2])
                return true;

            return false;
        }
        

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return A.GetHashCode() + B.GetHashCode() + C.GetHashCode() + unitOfMeasure.GetHashCode();
            //return base.GetHashCode();
        }



        /*
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
