using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NOTPOPPROTOTYPE
{
    class StaticStructure
    {
        protected Texture2D t;
        protected Rectangle bounds;
        protected Vector2 position;
        protected bool isSolid; // Player cannot move through it
        protected bool ground;

        public StaticStructure()
        {
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return bounds;
            }
        }

        public bool Solid
        {
            get
            {
                return isSolid;
            }
        }

        public bool IsGround
        {
            get
            {
                return ground;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t, position, Color.White);
        }
    }
}
