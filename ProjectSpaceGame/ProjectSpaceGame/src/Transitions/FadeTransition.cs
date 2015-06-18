using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSpaceGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSpaceGame.Transitions {
    public class FadeTransition : Transition {
        private float _timer, _totalTime;
        private bool _increasing;
        private Texture2D _fade;

        public FadeTransition(string state, SpaceGame game, float time)
            : base(state, game) {
            _totalTime = time;
            _timer = 0.0f;
            _increasing = true;
            // Get the texture used to fill the screen.
            _fade = Resource<Texture2D>.Get("TransitionTexture");
        }

        // Resets the effect.
        public override void Stop() {
            base.Stop();
            _increasing = true;
            _timer = 0.0f;
        }

        // Progress the transition by dt seconds.
        public override void Animate(float dt) {
            base.Animate(dt);
            // Checks if we're active.
            if (_isActive) {
                _timer += dt;

                if (_timer > _totalTime) {
                    // Are we done?
                    if (!_increasing) {
                        Stop();
                        return;
                    }

                    // Flip the effect.
                    _increasing = !_increasing;
                    _timer = 0.0f;
                    _game.ChangeState(_stateTo, _reset);
                }
            }
        }

        public override void DrawGUI(SpriteBatch spriteBatch) {
            base.DrawGUI(spriteBatch);
            if (_isActive) {
                float alpha = _timer / _totalTime;
                if (!_increasing) alpha = 1.0f - alpha;

                // Draws a black rectangle over the whole screen.
                spriteBatch.Draw(_fade, new Rectangle(0, 0, RenderParams.SIZE_X, RenderParams.SIZE_Y), new Color(1, 1, 1, alpha));
            }
        }
    }
}
