using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessiahSandbox
{
    class Satellite : CelestialBody
    {
        

        private string _name;

        private double _radius;
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
        /// The celestial body that the satellite orbits.
        /// </param>
        public Satellite(string name, double mass, double radius, double eccentricity, double semiMajorAxis, Texture2D texture, CelestialBody centralBody) {
            uc = new UnitConverter();


            _name = name;
            _mass = mass;
            _radius = radius;
            _eccentricity = eccentricity;
            _semiMajorAxis = semiMajorAxis;
            _texture = texture;
            _centralBody = centralBody;

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

            _position.X = _focus1.X - (float)_fociDist + (float)(_center.X + _semiMajorAxis * Math.Cos(_fociAngle)) / METERS_PER_PIXELS;
            _position.Y = _focus1.Y + (float)(_center.Y + _semiMinorAxis * Math.Sin(_fociAngle)) / METERS_PER_PIXELS;
            _focus1 = _centralBody.Position;
            _focus2 = new Vector2(_centralBody.Position.X - ((float)(_apoapsis - _periapsis) / METERS_PER_PIXELS), _centralBody.Position.Y);
            _center = new Vector2((_focus1.X + _focus2.X) / 2, (_focus1.Y + _focus2.Y) / 2);

            double r = _semiMajorAxis * (1 - _eccentricity * _eccentricity) /
                                        (1 - _eccentricity * Math.Cos(_angle));
            _angle += MathHelper.ToRadians(GAME_TIME_SPEED / (float)_orbitalPeriod);

            double ct = Math.Cos(_angle);
            double st = Math.Sin(_angle);
            double cp = Math.Cos(_fociAngle);
            double sp = Math.Sin(_fociAngle);

            double x1 = r * ct - _fociDist / 2;
            double y1 = r * st;
            double x = x1 * cp - y1 * sp;
            double y = x1 * sp + y1 * cp;

            _position.X = _focus2.X + (float)x / METERS_PER_PIXELS;
            _position.Y = _focus1.Y + (float)y / METERS_PER_PIXELS;

            _info = "";
            _size = (float)(_radius / (METERS_PER_PIXELS * 540));
            _color = Color.Black;
        }
    }
}
