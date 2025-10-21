using SFML.Window;

namespace Invaders;

public class InputManager
{
    private Dictionary<Keyboard.Key, bool> KeyStates = new();

    public InputManager()
    {
        Program.window.KeyPressed += (sender, args) =>
        {
            KeyStates[args.Code] = true;
        };
        
        Program.window.KeyReleased += (sender, args) =>
        {
            KeyStates[args.Code] = false;
        };
    }

    public void Update(Scene scene)
    {
        foreach (var key in KeyStates)
        {
            if (key.Value)
            {
                scene.Events.PublishInputHit(key.Key.ToString());
                KeyStates[key.Key] = false;
            }
        }
    }
}