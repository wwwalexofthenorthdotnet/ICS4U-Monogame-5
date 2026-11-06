using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.Versioning;

namespace ICS4U_Monogame_5
{
    enum Screen
    {
        Title,
        House,
        End
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Screen screen;
        Rectangle window;

        List<Texture2D> ghostTextures;

        Vector2 cursorPos;

        Texture2D mario;
        Texture2D background;
        Texture2D endBackground;
        Texture2D title;

        MouseState mouseState;
        Random generator;

        List<Ghost> ghosts;

        Ghost ghost1;

        KeyboardState keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 800, 600);

            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;

            _graphics.ApplyChanges();

            ghosts = new List<Ghost>();

            generator = new Random();

            ghostTextures = new List<Texture2D>();



            for (int i = 0; i < 20; i++)
            {
                int size = generator.Next(30, 51);
                ghosts.Add(new Ghost(ghostTextures, new Rectangle(generator.Next(window.Width - size), generator.Next(window.Height - size), size, size)));
            }

            screen = Screen.Title;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here

            ghostTextures.Add(Content.Load<Texture2D>("Images/boo-stopped"));

            background = Content.Load<Texture2D>("images/haunted-background");

            title = Content.Load<Texture2D>("images/haunted-title");

            endBackground = Content.Load<Texture2D>("images/haunted-end-screen");

            mario = Content.Load<Texture2D>("images/mario");
            for (int i = 1; i <= 8; i++)
                ghostTextures.Add(Content.Load<Texture2D>("Images/boo-move-" + i));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();


            if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                    screen = Screen.House;
            }
            else if (screen == Screen.House)
            {
                IsMouseVisible = false;

                cursorPos = new Vector2(mouseState.X, mouseState.Y);


                for (int i = 0; i < ghosts.Count; i++)
                {
                    ghosts[i].Update(gameTime, mouseState);
                    if (ghosts[i].Contains(mouseState.Position))
                        screen = Screen.End;
                }
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Title)
                _spriteBatch.Draw(title, window, Color.White);
            else if (screen == Screen.House)
            {
                
                _spriteBatch.Draw(background, window, Color.White);
                foreach (Ghost ghost in ghosts)
                    ghost.Draw(_spriteBatch);
                _spriteBatch.Draw(mario, cursorPos, Color.White);

            }
            else
                _spriteBatch.Draw(endBackground, window, Color.White);

            


            

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
