using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NOTPOPPROTOTYPE
{
    class Level
    {
        StaticStructure[] structs;

        public Level(StaticStructure[] s)
        {
            structs = s;
        }

        public void Draw(SpriteBatch spriteBatch){
            foreach (StaticStructure ss in structs)
            {
                if (ss == null)
                {
                    break;
                }
                spriteBatch.Draw(ss.Texture, ss.Position, Color.White);
            }
        }

        public StaticStructure[] Stuff
        {
            get
            {
                return structs;
            }
        }
    }
}
