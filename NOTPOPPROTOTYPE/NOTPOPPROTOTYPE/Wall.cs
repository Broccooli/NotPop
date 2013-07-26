using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NOTPOPPROTOTYPE
{
    class Wall : StaticStructure
    {
        public Wall(Texture2D t, Vector2 position)
        {
            base.ground = false;
            base.isSolid = true;
            base.t = t;
            base.position = position;
            base.bounds = new Rectangle((int)position.X,
                                        (int)position.Y,
                                        t.Width,
                                        t.Height);
        }


    }
}
