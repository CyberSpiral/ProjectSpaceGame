using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessiahSandbox
{
    class Star
    {
        private string _name;
        private string _type;
        private Color _color;

        private double _mass;
        private double _radius;
        private double _volume;
        private double _density;
        private double _gravity;
        private double _luminosity;
        private double _temperature;

        private Vector2 _position;
        private Vector2 _origin;

        private Texture2D _texture;


        /// <summary>
        /// Creates a star with realistic values all based around the mass and radius.
        /// </summary>
        /// <param name="name">
        /// The name of the star.
        /// </param>
        /// <param name="mass">
        /// The mass of the star in kilograms.
        /// </param>
        /// <param name="radius">
        /// The radius of the star in meters.
        /// </param>
        public Star(string name, double mass, double radius) {
            double pi = Math.PI;
            UnitConverter uc = new UnitConverter();

            _name = name;
            _mass = mass;
            _radius = radius;

            _volume = 4.0 / 3.0 * pi * Math.Pow(_radius, 3);
            _density = _mass / _volume;
            _gravity = 6.67 * Math.Pow(10, -11) * _mass / Math.Pow(_radius, 2) / 9.81;
            _luminosity = Math.Pow(_mass * uc.TO_SOLAR_MASS, 3.5);
            _temperature = 5775 * Math.Sqrt(Math.Sqrt(_luminosity / Math.Pow(_radius * uc.TO_SOLAR_RADIUS, 2)));

            _position = new Vector2(960, 540);

            #region Stellar Classification

            if (_temperature < 3700)
            {
                _type = "M";
                _color = Color.OrangeRed;
            }
            else if (_temperature < 5200)
            {
                _type = "K";
                _color = Color.Orange;
            }
            else if (_temperature < 6000)
            {
                _type = "G";
                _color = Color.LightYellow;
            }
            else if (_temperature < 7500)
            {
                _type = "F";
                _color = Color.White;
            }
            else if (_temperature < 10000)
            {
                _type = "A";
                _color = Color.LightCyan;
            }
            else if (_temperature < 30000)
            {
                _type = "B";
                _color = Color.LightBlue;
            }
            else
            {
                _type = "O";
                _color = Color.LightSkyBlue;
            }
            #endregion


            Console.WriteLine(
                "Name: {0}\n" +
                "Type: {1}-Class\n" +
                "Mass (Solar Mass): {2}\n" +
                "Radius (Solar Radius): {3}\n" +
                "Diameter (KM): {4}\n" +
                "Density (g/cm^3): {5}\n" +
                "Gravity (G): {6}\n" +
                "Luminosity (Solar Luminosity): {7}\n" +
                "Temperature (Kelvin): {8}\n"
                , _name, _type, _mass * uc.TO_SOLAR_MASS, _radius * uc.TO_SOLAR_RADIUS, _radius * 2, _density * uc.TO_GRAM_PER_CUBIC_CENTIMETER, _gravity, _luminosity, _temperature);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, _position, null, Color.Orange, 0, _origin, 1, SpriteEffects.None, 0.5f);

        }

        public Texture2D Texture
        {
            set
            {
                _texture = value;
                _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            }
        }

        public double Mass
        {
            get { return this._mass; }
        }
        public Vector2 Position
        {
            get { return this._position; }
        }
        
        public double Gravity
        {
            get { return this._gravity; }
        }
    }
}
