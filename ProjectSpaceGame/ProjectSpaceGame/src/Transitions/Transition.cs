using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSpaceGame.Transitions {
    /// <summary>
    /// Generic transition class that can handle all forms of screen/GUI transitions.
    /// </summary>
    public abstract class Transition {
        protected string _stateTo;
        protected bool _isActive;
        protected bool _reset;
        protected SpaceGame _game;

        public Transition(string stateTo, SpaceGame game) {
            _stateTo = stateTo;
            _isActive = false;
            _game = game;
        }

        public virtual void Start() { _isActive = true; }
        public virtual void Pause() { _isActive = false; }
        public virtual void Stop() { _isActive = false; }

        public virtual void Animate(float dt) { }
        public virtual void DrawGUI(SpriteBatch spriteBatch) { }

        public string StateTo { get { return _stateTo; } set { _stateTo = value; } }
        public bool Reset { get { return _reset; } set { _reset = value; } }
        public bool IsActive { get { return _isActive; } }
    }
}
