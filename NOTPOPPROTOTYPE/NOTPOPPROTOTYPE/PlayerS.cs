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
        Vector2 velocity = Vector2.Zero;
        Vector2 oldV = Vector2.Zero;
        Rectangle bounds;
        Rectangle screen;
        float speed = 1f;
        float maxSpeed = 10f;
        float friction = .93f;
        bool jumping = false;
        bool grounded;
        float jumpSpeed = 20f;

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
            oldV = velocity;
            velocity = Vector2.Zero;            
            oldP = position;

            keyboardState = Keyboard.GetState();
            
            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X = -1; // Going Left
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X = 1; // Going Right
            }

            if (keyboardState.IsKeyDown(Keys.Space) || jumping){
                velocity.Y = -1; // Move up

                if (!jumping)
                    jumping = true;
            }

            if (jumping)
            {
                velocity.Y *= jumpSpeed;
                jumpSpeed -= 1;
            }
            else
            {
                velocity.Y = 0;
                jumpSpeed = 20f;
            }

            if (velocity.X != 0) // Player is moving (i.e. Has W or A held down)
            {
                if (speed >= maxSpeed)
                {
                    speed = maxSpeed;
                }
                else
                {
                    speed += .3f;
                }

                velocity.X *= speed;
            }
            else
            {
                speed *= friction;

                // Apply friction in the correct direction
                // i.e. Going left, slow down while going left
                //
                // Without this, the player would always start moving right
                // when friction was applied, because friction is always a positive number
                //
                // This code simply checks the direction the player was moving in prior to stopping movement
                if (oldV.X < 0)
                {
                    velocity.X = -speed;
                }
                else if (oldV.X > 0){
                    velocity.X = speed;
                }
            }            

            position += velocity;

            bounds = new Rectangle((int)position.X,
                                   (int)position.Y,
                                   t.Width,
                                   t.Height);

            if (isColliding(ssa))
            {
                position = oldP;
                bounds = this.Bounds;
                velocity.X = 0;
            }
        }



        public void setInStartPosition()
        {
            position.X = (screen.Width - t.Width) / 2 + 150;
            position.Y = (screen.Height - t.Height) / 4 + 150;
        }

        public bool isColliding(StaticStructure[] ssa)
        {
            foreach (StaticStructure ss in ssa)
            {
                if (ss == null)
                {
                    break;
                }
                if (bounds.Intersects(ss.Bounds))
                {
                    if (velocity.Y < 0) // Player is moving up
                    {
                        velocity.Y = 0; // Stop the player from moving up
                    }
                    else if (velocity.Y > 0 && bounds.Bottom > ss.Bounds.Top){ // Moving down
                        jumping = false;
                        grounded = true;
                    }
                    return true;
                }
            }

            return false;
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

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t, position, Color.White);
        }
    }
}
