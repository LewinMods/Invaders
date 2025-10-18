
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Invaders 
{
    class Program 
    {
        public static uint ScreenWidth = 600, ScreenHeight = 900;
        
        static void Main(string[] args) 
        {
            using (var window = new RenderWindow(
                       new VideoMode(ScreenWidth, ScreenHeight), "Invaders")) 
            {
                window.Closed += (o, e) => window.Close();
                
                window.SetFramerateLimit(60);

                Scene scene = new Scene();

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
}