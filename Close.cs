using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Close : GUI
{
    public Close() : base("button")
    {
        title = "";
    }

    public override void Create(Scene scene)
    {
        buttonAmount = 0;
        
        scene.MusicHandler.music.Stop();
        scene.MusicHandler.music.Dispose();
        
        Program.window.Close();
    }
}