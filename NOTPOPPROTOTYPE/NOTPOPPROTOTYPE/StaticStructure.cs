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
        protected Rectangle bounds = new Rectangle();
        protected Vector2 position;
        protected bool isSolid; // Player cannot move through it

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

        public float X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
                bounds.X = (int)value;
            }
        }

        public float Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
                bounds.Y = (int)value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return t;
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t, position, Color.White);
        }
    }
}
