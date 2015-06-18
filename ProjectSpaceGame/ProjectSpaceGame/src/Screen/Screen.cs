using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSpaceGame.Screens {
    /// <summary>
    /// Base class for all out game's "screens" (or gamestates).
    /// All other screens should inherit from this class.
    /// </summary>
    public class Screen {
        public bool IsLoaded = false;
        public Screen() { }
        public virtual void Initialize() { }
        public virtual void Load() { }
        public virtual void Unload() { }
        public virtual void Update(float deltaTime) { }
        public virtual void Draw(SpriteBatch spriteBatch, float deltaTime) { }
    }
}
