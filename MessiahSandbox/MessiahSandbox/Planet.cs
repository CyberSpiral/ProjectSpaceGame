using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessiahSandbox
{
    class Planet
    {
        private string _name;
        private string _type;
        private Color _color;

        private double _mass;
        private double _radius;
        private double _gravity;
        private double _density;
        private double _escapeVelocity;
        private double _eccentricity;
        private double _semiMajorAxis;
        private double _semiMinorAxis;
        private double _periapsis;
        private double _apoapsis;
        private double _orbitalPeriod;
        private double _orbitalVelocity;

        Star _star;

        /// <summary>
        /// Creates a planet with realistic properties based on masss, radius, eccentricity and its semi-major axis and star.
        /// </summary>
        /// <param name="name">
        /// Name of the planet.
        /// </param>
        /// <param name="mass">
        /// Mass of the planet in kilograms.
        /// </param>
        /// <param name="radius">
        /// Radius of the planet in meters.
        /// </param>
        /// <param name="eccentricity">
        /// Eccentricity of the planet's orbit.
        /// </param>
        /// <param name="semiMajorAxis">
        /// The semi-major axis of the planet's orbit in Astronomical Units.
        /// </param>
        /// <param name="star"></param>
        public Planet(string name, double mass, double radius, double eccentricity, double semiMajorAxis, Star star) {
            double pi = Math.PI;
            UnitConverter uc = new UnitConverter();

            _name = name;
            _mass = mass;
            _radius = radius;
            _eccentricity = eccentricity;
            _semiMajorAxis = semiMajorAxis;
            _star = star;

            _gravity = _mass * uc.TO_EARTH_MASS / Math.Pow(_radius * uc.TO_EARTH_RADIUS, 2);
            _density = _gravity / (_radius * uc.TO_EARTH_RADIUS) * uc.FROM_EARTH_DENSITY;
            _escapeVelocity = Math.Sqrt(_mass * uc.TO_EARTH_MASS / (_radius * uc.TO_EARTH_RADIUS)) * 11.2;

            _semiMinorAxis = _semiMajorAxis * Math.Sqrt(1 - Math.Pow(_eccentricity, 2));
            _periapsis = _semiMajorAxis * (1 - _eccentricity);
            _apoapsis = _semiMajorAxis * (1 + _eccentricity);
            _orbitalPeriod = Math.Sqrt(Math.Pow(_semiMajorAxis, 3) / (_star.Mass * uc.TO_SOLAR_MASS));
            _orbitalVelocity = Math.Sqrt(_star.Mass * uc.TO_SOLAR_MASS / _semiMajorAxis) * 29.78;


            Console.WriteLine(
                "Name: {0}\n" +
                "Mass (Earth Mass): {1}-Class\n" +
                "Radius (Earth Radius): {2}\n" +
                "Diameter (km): {3}\n" +
                "Density (g/cm^3): {4}\n" +
                "Gravity (G): {5}\n" +
                "Escape Velocity (km/s): {6}\n" +
                "Eccentricity: {7}\n" +
                "Semi-Major Axis (AU): {8}\n" +
                "Semi-Minor Axis (AU): {9}\n" +
                "Periapsis (AU): {10}\n" +
                "Apoapsis (AU): {11}\n" +
                "Orbital Period (Years): {12}\n" +
                "Orbital velocity: {13}\n"
                , _name, _mass * uc.TO_EARTH_MASS, _radius * uc.TO_EARTH_RADIUS, _radius * 2 * uc.TO_KILOMETER, _density * uc.TO_GRAM_PER_CUBIC_CENTIMETER,
                _gravity, _escapeVelocity, _eccentricity, _semiMajorAxis, _semiMinorAxis, _periapsis, _apoapsis, _orbitalPeriod, _orbitalVelocity);
        }
    }
}
