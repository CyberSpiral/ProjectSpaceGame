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

namespace MessiahSandbox {
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Star _starTest;
        Planet _planetTest;
        Texture2D _celBodyTexture;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
        }

        protected override void Initialize() {
            //Star with nearly identical values to our sun.
            _starTest = new Star("The Sun", 1.98855 * Math.Pow(10, 30), 6.955 * Math.Pow(10, 8));
            //Planet with nearly identical values to Earth.
            _planetTest = new Planet("Earth", 5.97219 * Math.Pow(10,24), 6378000, 0.01671, 149597870700, _starTest);
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _celBodyTexture = Content.Load<Texture2D>("CelBody");
            _starTest.Texture = _celBodyTexture;
            _planetTest.Texture = _celBodyTexture;
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
            _planetTest.Update();
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin();
            _starTest.Draw(_spriteBatch);
            _planetTest.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
