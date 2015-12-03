using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ProjectSpaceGame;
using ProjectSpaceGame.Utils;

namespace MessiahSandbox {
    public class Game1 : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Star _theSun;
        private List<Planet> _planetList;
        private Texture2D _celBodyTexture;
        private Camera _camera;
        private KeyboardState _keyboard;
        private float _previousScrollValue;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _camera = new Camera();
            _camera.Zoom = 0.00005F;

            Globals.font1 = Content.Load<SpriteFont>("TempFont");
            _celBodyTexture = Content.Load<Texture2D>("CelBody");

            _theSun = new Star("The Sun", 1.98855 * Math.Pow(10, 30), 6.955 * Math.Pow(10, 8), _celBodyTexture);

            _planetList = new List<Planet>();
            _planetList.Add(new Planet("Mercury", 3.3 * Math.Pow(10, 23), 2439700, 0.2056, 57909050000, _celBodyTexture, _theSun));
            _planetList.Add(new Planet("Venus", 4.8676 * Math.Pow(10, 24), 6051800, 0.0067, 108208000000, _celBodyTexture, _theSun));
            _planetList.Add(new Planet("Earth", 5.97219 * Math.Pow(10, 24), 6378000, 0.01671, 149597870700, _celBodyTexture, _theSun));
            _planetList.Add(new Planet("Mars", 6.4185 * Math.Pow(10, 11), 3376200, 0.0935, 227939100000, _celBodyTexture, _theSun));
            _planetList.Add(new Planet("Jupiter", 1.8986 * Math.Pow(10, 27), 69911000, 0.0487, 778547200000, _celBodyTexture, _theSun));
            _planetList.Add(new Planet("Saturn", 5.6846 * Math.Pow(10, 26), 58232000, 0.0557, 1433449370000, _celBodyTexture, _theSun));
            _planetList.Add(new Planet("Uranus", 8.681 * Math.Pow(10, 25), 25362000, 0.0472, 2870671400000, _celBodyTexture, _theSun));
            _planetList.Add(new Planet("Neptune", 1.0243 * Math.Pow(10, 26), 24622000, 0.0086, 4498542600000, _celBodyTexture, _theSun));


        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
            
            _keyboard = Keyboard.GetState();

            _theSun.Update(gameTime);
            foreach(Planet planet in _planetList)
            {
                planet.Update(gameTime);
            }

            _camera.Position = _theSun.Position;//_planetList[0].Position;


            if (_keyboard.IsKeyDown(Keys.W))
            {
                _camera.Move(new Vector2(0, -5 / _camera.Zoom)); 
            }
            if (_keyboard.IsKeyDown(Keys.A))
            {
                _camera.Move(new Vector2(-5 / _camera.Zoom, 0));
            }
            if (_keyboard.IsKeyDown(Keys.S))
            {
                _camera.Move(new Vector2(0, 5 / _camera.Zoom));
            }
            if (_keyboard.IsKeyDown(Keys.D))
            {
                _camera.Move(new Vector2(5 / _camera.Zoom, 0));
            }
            if (_previousScrollValue > Mouse.GetState().ScrollWheelValue)
            {
                _camera.Zoom -= _camera.Zoom / 10;
                Console.WriteLine("Zoom: " + _camera.Zoom);
            }
            if (_previousScrollValue < Mouse.GetState().ScrollWheelValue)
            {
                _camera.Zoom += _camera.Zoom / 10;
                Console.WriteLine("Zoom: " + _camera.Zoom);
            }

            _previousScrollValue = Mouse.GetState().ScrollWheelValue;
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, _camera.get_transformation(_graphics.GraphicsDevice));
            _theSun.Draw(_spriteBatch);
            foreach (Planet planet in _planetList)
            {
                planet.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ResetCamera()
        {
            _camera.Move(new Vector2(RenderParams.SIZE_X / 2, RenderParams.SIZE_Y / 2));
        }
    }
}
