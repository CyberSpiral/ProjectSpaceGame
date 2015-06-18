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
using ProjectSpaceGame.Screens;
using ProjectSpaceGame.Utils;
using ProjectSpaceGame.Transitions;

namespace ProjectSpaceGame {
    public class SpaceGame : Game {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Dictionary<string, Screen> _gameStates;
        string _gameState = "None";

        Transition _stateTransition;

        public SpaceGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Base screen.
            _gameStates = new Dictionary<string, Screen>();
            _gameStates["None"] = new Screen();

            // Hardcoded screen size for now.
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO(Bearish): Temporary.
            Texture2D fadeTexture = new Texture2D(GraphicsDevice, 1, 1);
            fadeTexture.SetData(new Color[] { Color.Black });
            Resource<Texture2D>.Set("TransitionTexture", fadeTexture);
            _stateTransition = new FadeTransition("None", this, 1);
            QueryStateChange("None");
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
            // Update the current screen and the transition.
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _gameStates[_gameState].Update(dt);
            _stateTransition.Animate(dt);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _gameStates[_gameState].Draw(_spriteBatch, dt);

            _stateTransition.DrawGUI(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void QueryStateChange(string state, bool reset = false) {
            if (state == "Exit") Exit();

            _stateTransition.StateTo = state;
            _stateTransition.Reset = reset;
            _stateTransition.Stop();
            _stateTransition.Start();
        }

        public void ChangeState(string state, bool reset) {
            if (_gameStates[state].IsLoaded == false) {
                _gameStates[state].Load();
                _gameStates[state].IsLoaded = true;
                _gameStates[state].Initialize();
            }

            if (reset) {
                _gameStates[state].Initialize();
            }

            _gameState = state;
        }
    }
}
