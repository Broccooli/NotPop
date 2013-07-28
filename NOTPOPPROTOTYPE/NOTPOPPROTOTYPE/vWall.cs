using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NOTPOPPROTOTYPE
{
    class vWall : StaticStructure
    {
        public vWall(Texture2D t, Vector2 position)
        {
            base.isSolid = true;
            base.t = t;
            base.position = position;
            base.bounds = new Rectangle((int)position.X,
                                        (int)position.Y,
                                        t.Width,
                                        t.Height);
        }

        public vWall(Texture2D t)
        {
            base.isSolid = true;
            base.t = t;
            base.bounds.Width = t.Width;
            base.bounds.Height = t.Height;
        }

    }
}
