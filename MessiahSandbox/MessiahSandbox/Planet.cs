using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessiahSandbox
{
    class Planet : CelestialBody
    {

        private string _name;
        private string _type;

        private double _radius;
        private double _density;
        private double _escapeVelocity;
        private double _orbitalVelocity;

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
        /// The semi-major axis of the planet's orbit in meters.
        /// </param>
        /// <param name="distanceMultiplier">
        /// How many times further it apears to be from the central body.
        /// </param>
        /// <param name="centralBody">
        /// The celestial body that the planet orbits.
        /// </param>
        public Planet(string name, double mass, double radius, double eccentricity, double semiMajorAxis, Texture2D texture, CelestialBody centralBody) {
            uc = new UnitConverter();

            _name = name;
            _mass = mass;
            _radius = radius;
            _eccentricity = eccentricity;
            _semiMajorAxis = semiMajorAxis;
            _texture = texture;
            _centralBody = centralBody;

            _gravity = _mass * uc.TO_EARTH_MASS / Math.Pow(_radius * uc.TO_EARTH_RADIUS, 2);
            _density = _gravity / (_radius * uc.TO_EARTH_RADIUS) * uc.FROM_EARTH_DENSITY;
            _escapeVelocity = Math.Sqrt(_mass * uc.TO_EARTH_MASS / (_radius * uc.TO_EARTH_RADIUS)) * 11.2;

            _semiMinorAxis = _semiMajorAxis * Math.Sqrt(1 - Math.Pow(_eccentricity, 2));
            _periapsis = _semiMajorAxis * (1 - _eccentricity);
            _apoapsis = _semiMajorAxis * (1 + _eccentricity);
            _orbitalPeriod = Math.Sqrt(Math.Pow(_semiMajorAxis * uc.TO_AU, 3) / (_centralBody.Mass * uc.TO_SOLAR_MASS));
            _orbitalVelocity = Math.Sqrt(_centralBody.Mass * uc.TO_SOLAR_MASS / _semiMajorAxis) * 29.78;
            _ellipseArea = Math.PI * (_semiMajorAxis * _semiMinorAxis) / METERS_PER_PIXELS;

            _origin = Vector2.Zero;

            _angle = 0;

            _focus1 = _centralBody.Position;
            _focus2 = new Vector2(_centralBody.Position.X - ((float)(_apoapsis - _periapsis) / METERS_PER_PIXELS), _centralBody.Position.Y);

            _fociDist = Vector2.Distance(_focus2, _focus1);
            _fociAngle = MathHelper.ToRadians(180) + Math.Atan2(_focus2.Y - _focus1.Y, _focus2.X - _focus1.X);
            _center = new Vector2((_focus1.X + _focus2.X) / 2, (_focus1.Y + _focus2.Y) / 2);

            _position.X = _focus2.X + (float)_fociDist + (float)(_center.X + _semiMajorAxis * Math.Cos(_fociAngle)) / METERS_PER_PIXELS;
            _position.Y = _focus1.Y + (float)(_center.Y + _semiMinorAxis * Math.Sin(_fociAngle)) / METERS_PER_PIXELS;
            _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);


            _info =
                "Name: " + _name +
                "\nMass (Earth Mass): " + _mass * uc.TO_EARTH_MASS +
                "\nRadius (Earth Radius): " + _radius * uc.TO_EARTH_RADIUS +
                "\nDiameter (km): " + _radius * 2 * uc.TO_KILOMETER +
                "\nDensity (g/cm^3): " + _density * uc.TO_GRAM_PER_CUBIC_CENTIMETER +
                "\nGravity (G): " + _gravity +
                "\nEscape Velocity (km/s): " + _escapeVelocity +
                "\nEccentricity: " + _eccentricity +
                "\nSemi-Major Axis (AU): " + _semiMajorAxis * uc.TO_AU +
                "\nSemi-Minor Axis (AU): " + _semiMinorAxis * uc.TO_AU +
                "\nPeriapsis (AU): " + _periapsis * uc.TO_AU +
                "\nApoapsis (AU): " + _apoapsis * uc.TO_AU +
                "\nOrbital Period (Years): " + _orbitalPeriod +
                "\nOrbital velocity: " + _orbitalVelocity;
            _size = (float)(_radius / (METERS_PER_PIXELS * 540));
            _size = 1000;
            _color = Color.Blue;

            Console.WriteLine(
                "Name: {0}\n" +
                "Mass (Earth Mass): {1}\n" +
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
                "Orbital velocity: {13}\n",
                _name, _mass * uc.TO_EARTH_MASS, _radius * uc.TO_EARTH_RADIUS, _radius * 2 * uc.TO_KILOMETER, _density * uc.TO_GRAM_PER_CUBIC_CENTIMETER,
                _gravity, _escapeVelocity, _eccentricity, _semiMajorAxis * uc.TO_AU, _semiMinorAxis * uc.TO_AU, _periapsis * uc.TO_AU, _apoapsis * uc.TO_AU,
                _orbitalPeriod, _orbitalVelocity);

        }
    }
}
