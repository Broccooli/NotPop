using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace NOTPOPPROTOTYPE
{
    class PlayerS
    {
        static bool hasInstance = false;
        static PlayerS inst;
        Vector2 position;
        Vector2 oldP;
        Vector2 motion;
        Rectangle bounds;
        Rectangle screen;
        float speed = 6f;
        float maxFallSpeed = 20f;
        float fallSpeed = 0f;

        float jumpSpeed = 10f;
        float jumpInterval = 1f;

        bool grounded = false;
        bool jumping = false;

        Texture2D t;
        KeyboardState keyboardState;

        private PlayerS(ContentManager c, Rectangle SB)
        {
            t = c.Load<Texture2D>("hero");
            screen = SB;
            setInStartPosition();
        }

        public static PlayerS Init(ContentManager c, Rectangle ScreenBounds)
        {
            if (hasInstance){
                Console.Write("We have an instance");
                return inst;
            }
            else{
                Console.Write("Creating instance...");
                hasInstance = true;
                inst = new PlayerS(c, ScreenBounds);
                return inst;
            }
        }

        public static PlayerS Instance
        {
            get
            {
                if (hasInstance)
                {
                    return inst;
                }
                else
                {
                    throw new ContentLoadException("No Instance of Player to be gotten");
                }
            }
        }

        public void Update(StaticStructure[] ssa)
        {
            motion = Vector2.Zero;
            oldP = position;

            keyboardState = Keyboard.GetState();

            //if (jumping)
            //{
            //    motion.Y = -1;
            //    jumpSpeed -= 1;
            //}

            
            if (keyboardState.IsKeyDown(Keys.A))
            {
                motion.X = -1;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                motion.X = 1;
            }

            //if (keyboardState.IsKeyDown(Keys.Space))
            //{
            //    jumping = true;
            //}

            motion.X *= speed;

            if (!grounded)
            {
                fall(); // Sets motion.Y so that the player falls
            }

            position += motion;

            bounds = new Rectangle((int)position.X,
                                   (int)position.Y,
                                   t.Width,
                                   t.Height);


            if (isColliding(ssa))
            {
                if (!grounded)
                {
                    position.X = oldP.X;
                }
                else
                {
                    position = oldP;
                }

                bounds = this.Bounds;
            }

        }

        private void fall()
        {
            fallSpeed += 0.5f;
            motion.Y = 1; 

            if (fallSpeed >= maxFallSpeed)
            {
                fallSpeed = maxFallSpeed;
            }

            motion.Y *= fallSpeed;

        }

        public void setInStartPosition()
        {
            position.X = (screen.Width - t.Width) / 2 ;
            position.Y = (screen.Height - t.Height) / 4;
        }

        public Rectangle Bounds
        {
            get
            {
                bounds.X = (int)position.X;
                bounds.Y = (int)position.Y;
                return bounds;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

        }

        public bool isColliding(params StaticStructure[] ssa)
        {
            // Rectangle check = new Rectangle((int)position.X, (int)position.Y, t.Width, t.Height + 10);

            foreach (StaticStructure ss in ssa)
            {
                if (ss.IsGround)
                {
                    //if (check.Intersects(ss.Bounds) && !grounded)
                    if(((Ground)ss).hasPlayer())
                    {
                        grounded = true;
                        fallSpeed = 0f;
                    }
                    else
                    {
                        grounded = false;
                    }
                }
                
                if (bounds.Intersects(ss.Bounds) && ss.Solid)
                {
                    return true;
                }
            }

            
            return false;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t, position, Color.White);
        }
    }
}
