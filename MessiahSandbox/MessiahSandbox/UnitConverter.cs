using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessiahSandbox
{
    class UnitConverter
    {
        /*
         * Standard Units:
         * 
         * Length : Meter
         * Mass : Kilogram
         * Volume : Cubic Metre
         * Density : Kilogram / Cubic Meter
         * 
        */

        //Length
        public readonly double TO_SOLAR_RADIUS = 1.4368 * Math.Pow(10,-9);
        public readonly double TO_EARTH_RADIUS = 1.5679 * Math.Pow(10, -7);
        //public readonly double TO_JUPITER_RADIUS = 1.4304 * Math.Pow(10, -8);
        public readonly double TO_KILOMETER = 0.001;
        public readonly double TO_CENTIMETER = 100;
        public readonly double TO_AU = 6.6846 * Math.Pow(10, -12);

        public readonly double FROM_SOLAR_RADIUS = 696342000;
        public readonly double FROM_EARTH_RADIUS = 6378100;
        public readonly double FROM_KILOMETER = 1000;
        public readonly double FROM_CENTIMETER = 100;

        //Mass
        public readonly double TO_SOLAR_MASS = 5 * Math.Pow(10, -31);
        public readonly double TO_EARTH_MASS = 1.6744 * Math.Pow(10, -25);
        //public readonly double TO_JUPITER_MASS = 5.2659 * Math.Pow(10, -28);
        public readonly double TO_GRAM = 1000;

        public readonly double FROM_SOLAR_MASS = 1.98855 * Math.Pow(10, 30);
        public readonly double FROM_EARTH_MASS = 5.97219 * Math.Pow(10, 24);
        public readonly double FROM_GRAM = 0.001;

        //Volume
        public readonly double TO_CUBIC_KILOMETER = 1 * Math.Pow(10, -9);
        public readonly double TO_CUBIC_CENTIMETER = 1000000;

        //Density
        public readonly double TO_GRAM_PER_CUBIC_CENTIMETER = 0.001;

        public readonly double FROM_EARTH_DENSITY = 5514;
        public readonly double FROM_GRAM_PER_CUBIC_CENTIMETER = 1000;

        //Velocity
        public readonly double TO_METER_PER_SECOND = 9.81;
        public readonly double TO_KILOMETER_PER_SECOND = 0.00981;
    }
}
