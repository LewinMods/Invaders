using SFML.Window;

namespace Invaders;

public class InputManager
{
    private Dictionary<string, bool> previousKeyStates = new();
    private List<string> keys;

    public InputManager(List<string> keys)
    {
        this.keys = keys;
        
        foreach (var key in keys)
        {
            previousKeyStates[key] = false;
        }
    }

    public void Update(Scene scene)
    {
        foreach (var key in keys)
        {
            if (Enum.TryParse(key, true, out Keyboard.Key keyEnum))
            {
                bool isPressed = Keyboard.IsKeyPressed(keyEnum);
                bool wasPressed = previousKeyStates[key];
                
                if (isPressed && !wasPressed)
                {
                    scene.Events.PublishInputHit(key);
                }
                
                previousKeyStates[key] = isPressed;
            }
        }
    }
}