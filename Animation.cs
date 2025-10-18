using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Animation
{
    private List<IntRect> frames;
    private IntRect chosen;
    private Clock clock;

    private float time;
    private int iteration = 0;

    private static float frameTime = 50;
    
    public Animation(List<IntRect> frames)
    {
        clock = new Clock();
        
        this.frames = frames;

        chosen = frames[0];

        time = frameTime;
    }

    public IntRect UpdateAnimation()
    {
        time -= clock.ElapsedTime.AsMilliseconds();
        clock.Restart();
        
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

    public void ChangeFrames(List<IntRect> newFrames)
    {
        if (newFrames != null && newFrames != frames)
        {
            frames = newFrames;
            iteration = 0;
        }
    }
}