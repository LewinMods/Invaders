using SFML.Audio;

namespace Invaders;

public class MusicHandler
{
    //public Sound music;
    public Music music;
    
    public MusicHandler(Scene scene, string name)
    {
        music = scene.Assets.LoadSounds(name);
        music.Loop = true;
        music.Play();
    }
}