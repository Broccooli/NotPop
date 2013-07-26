using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NOTPOPPROTOTYPE
{
    class Ground : StaticStructure
    {
        public Ground(Texture2D t, Vector2 position)
        {
            base.ground = true;
            base.isSolid = true;
            base.t = t;
            base.position = position;
            base.bounds = new Rectangle((int)position.X,
                                        (int)position.Y,
                                        t.Width,
                                        t.Height);
        }

        public bool hasPlayer(Rectangle check)
        {          

            if (check.Intersects(bounds) && PlayerS.Instance.Bounds.Bottom < position.Y)
            {
                return true;
            }

            return false;
        }

    }
}
