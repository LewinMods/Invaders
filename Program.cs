
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Invaders;
    
    class Program 
    {
        public static RenderWindow window;
        
        public static uint ScreenWidth = 600, ScreenHeight = 900;
        
        static void Main(string[] args) 
        {
            using (window = new RenderWindow(
                       new VideoMode(ScreenWidth, ScreenHeight), "Invaders"))
            {
                Scene scene = new Scene();
                
                window.Closed += (o, e) =>
                {
                    scene.MusicHandler.music.Stop();
                    scene.MusicHandler.music.Dispose();
                    window.Close();
                };
                
                window.SetFramerateLimit(60);

                Clock clock = new Clock();
                
                while (window.IsOpen) 
                {
                    window.DispatchEvents();
                    
                    float deltaTime = clock.Restart().AsSeconds();
                    deltaTime = MathF.Min(deltaTime, 0.1f);

                    scene.UpdateAll(deltaTime);

                    window.Clear(new Color(7, 0, 15));

                    scene.RenderAll(window);

                    window.Display();
                }
            }
        }
    }