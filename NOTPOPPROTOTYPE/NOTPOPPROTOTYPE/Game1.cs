using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NOTPOPPROTOTYPE
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const int NUM_STRUCTS = 3;
        const int NUM_LVLS = 5;
        const int MAX_STRUCTS_PER_LEVEL = 100;
        int currLevel = 1;

        PlayerS player;
        Rectangle screenRectangle;

        SpriteFont font;

        Level[] Levels = new Level[NUM_LVLS];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 750;
            graphics.PreferredBackBufferHeight = 600;

            screenRectangle = new Rectangle(
                0,
                0,
                graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = PlayerS.Init(this.Content, screenRectangle);
            font = Content.Load<SpriteFont>("myFont");

            loadLevels();

            
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            player.Update(Levels[currLevel - 1].Stuff);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            player.Draw(spriteBatch);
            DrawText();
            Levels[currLevel - 1].Draw(spriteBatch);



            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawText()
        {
            spriteBatch.DrawString(font, "Player's Position: (" + (PlayerS.Instance.Position.X) + ", " + (PlayerS.Instance.Position.Y) + ")", new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, "Player's X Velocity: " + (PlayerS.Instance.Velocity.X), new Vector2(10, 20), Color.Black);
            spriteBatch.DrawString(font, "Player's Y Velocity: " + (PlayerS.Instance.Velocity.Y), new Vector2(10, 30), Color.Black);
            spriteBatch.DrawString(font, "Player's Bound Box: ", new Vector2(250, 10), Color.Black);
            spriteBatch.DrawString(font, "Top: " + PlayerS.Instance.Bounds.Top, new Vector2(300, 20), Color.Black);
        }

        private void loadLevels()
        {
            String line, type;
            short lvl;
            string[] structs;
            StaticStructure[] ssa;
            Texture2D t;
            int ii;

            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            StreamReader sr = new StreamReader(a.GetManifestResourceStream("NOTPOPPROTOTYPE.lvls.txt"));

            line = sr.ReadLine(); // Get the next line in the document
            do{           

                if( line.Contains('!') ){ // If the line contains '!' then it's the beginning of a new level

                    if (line.Length == 1)
                    {
                        break;
                    }

                    lvl = (short)Char.GetNumericValue(line.ElementAt<char>(1));
                    ssa = new StaticStructure[MAX_STRUCTS_PER_LEVEL];
                    ii = 0;
                    

                    while( sr.Peek() != '!' ){
                        line = sr.ReadLine();
                        structs = line.Split(':');                 
                        type = structs[0];
                        StaticStructure ss = new StaticStructure();

                        for (int yy = 0; yy < structs.Length; yy++){
                            if ( yy == 0 ){
                                t = Content.Load<Texture2D>(type);
                                
                                if (type == "hWall"){
                                    ss = new hWall(t);
                                }
                                else if(type == "vWall"){
                                    ss = new vWall(t);
                                }
                                else{
                                    Console.WriteLine("error in lvls.txt");
                                }
                            }
                            else if ( yy == 1 ){
                                ss.X = Convert.ToInt32(structs[yy]);
                            }
                            else if ( yy == 2 ){
                                ss.Y = Convert.ToInt32(structs[yy]);
                            }
                            else{
                                Console.WriteLine("More then needed in lvls.txt");
                            }
                        }

                        ssa[ii] = ss;
                        ii++;
                    }

                    Levels[lvl - 1] = new Level(ssa); 
                }

            } while ((line = sr.ReadLine()) != null);
        }
    }
}
