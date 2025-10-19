using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Animation
{
    private List<IntRect> frames;
    private IntRect chosen;
    private Clock clock;

    private float time;
    private static float frameTime = 50;
    
    private int iteration = 0;

    private bool oneLoop;
    
    public Animation(List<IntRect> frames, bool value)
    {
        clock = new Clock();
        
        this.frames = frames;
        chosen = frames[0];
        
        oneLoop = value;

        time = frameTime;
    }

    public IntRect UpdateAnimation()
    {
        if (oneLoop && iteration >= frames.Count)
        {
            clock.Dispose();
            
            return new IntRect();
        }
        else
        {
           time -= clock.ElapsedTime.AsMilliseconds();
           clock.Restart(); 
        }
        
        if (time <= 0)
        {
            if (iteration == frames.Count)
            {
                iteration = 0;
            }
            
            chosen = frames[iteration];
            iteration++;
            
            time = frameTime;
        }
        
        return chosen;
    }
}