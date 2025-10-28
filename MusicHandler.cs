using SFML.Audio;

namespace Invaders;

public class MusicHandler
{
    public Music music;
    
    public MusicHandler(Scene scene, string name)
    {
        music = scene.Assets.LoadSounds(name);
        music.Volume = 0.7f;
        music.Loop = true;
        music.Play();
    }
}