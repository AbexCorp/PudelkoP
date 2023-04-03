using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Pudelko.Enums;
using System.Collections;

namespace Pudelko
{
    //Constructor
    public sealed class Pudelko : IEquatable<Pudelko>, IEnumerable<double>//, IFormattable #FIX#
    {
        //To do
        //IFormatable
        //8 Operator +
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
            unitOfMeasure = unit;

            //This probably converts the numbers two times for some reason
            //dimensionArray[0] = UnitUtility.OneToAny(a, UnitOfMeasure);
            //dimensionArray[1] = UnitUtility.OneToAny(b, UnitOfMeasure);
            //dimensionArray[2] = UnitUtility.OneToAny(c, UnitOfMeasure);

            dimensionArray[0] = a;
            dimensionArray[1] = b;
            dimensionArray[2] = c;
        }



        //Variables
        private double a;
        private double b;
        private double c;
        private UnitOfMeasure unitOfMeasure;
        public double[] dimensionArray = new double[3];



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

        public Pudelko Current => throw new NotImplementedException();




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
            if (box is null || GetType() != box.GetType())
            {
                return false;
            }
            // TODO: write your implementation of Equals() here

            double[] boxA = UnitUtility.ThreeFromAnyToAny(A, B, C, UnitOfMeasure.meter, UnitOfMeasure.milimeter);
            double[] boxB = UnitUtility.ThreeFromAnyToAny(box.A, box.B, box.C, UnitOfMeasure.meter, UnitOfMeasure.milimeter);

            if (boxA[0] == boxB[0] && boxA[1] == boxB[1] && boxA[2] == boxB[2])
                return true;

            return false;
        }



        //HashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return HashCode.Combine(a, b, c);
            //return A.GetHashCode() + B.GetHashCode() + C.GetHashCode() + unitOfMeasure.GetHashCode();
            //return base.GetHashCode();
        }

        


        //Operators
        public static bool operator ==(Pudelko boxA, Pudelko boxB)
        {
            return boxA.Equals(boxB);
        }
        public static bool operator !=(Pudelko boxA, Pudelko boxB)
        {
            return !boxA.Equals(boxB);
        }



        //Conversions
        public static explicit operator double[](Pudelko box)
        {
            return new double[] { box.A, box.B, box.C };
        }
        public static implicit operator Pudelko(ValueTuple<int, int, int> tuple)
        {
            return new Pudelko(tuple.Item1, tuple.Item2, tuple.Item3, UnitOfMeasure.milimeter);
        }



        //Indexer
        public double this[int index]
        {
            get
            {
                if(index > 2 || index < 0)
                    throw new IndexOutOfRangeException();
                return dimensionArray[index];
            }
        }



        //Iterator
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < dimensionArray.Length; i++)
            {
                yield return dimensionArray[i];
            }
        }

        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            for (int i = 0; i < dimensionArray.Length; i++)
            {
                yield return dimensionArray[i];
            }
        }



        //Parse
        public static Pudelko Parse(string text)
        {
            //new P(2.5, 9.321, 0.1) == P.Parse("2.500 m × 9.321 m × 0.100 m")
            if (text == null)
                throw new ArgumentNullException();

            string[] textToParse = text.Split('×', StringSplitOptions.RemoveEmptyEntries);
            if (textToParse.Length != 3)
                throw new FormatException();

            double[] dimensions = ParseThreeNumbers(textToParse);
            UnitOfMeasure unit = ParseGetUnit(textToParse[0]);

            return new Pudelko(dimensions[0], dimensions[1], dimensions[2], unit);
        }

        private static UnitOfMeasure ParseGetUnit(string text) { throw new NotImplementedException(); }
        private static double[] ParseThreeNumbers(string[] text) { throw new NotImplementedException(); }
        private static double ParseNumber(string text) {throw new NotImplementedException();}

    }
}
