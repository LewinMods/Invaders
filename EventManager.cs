namespace Invaders;

public delegate void InputEvent(Scene scene, string key);

public class EventManager
{
    public event InputEvent InputHit;

    private string inputHit;
    
    public EventManager()
    {
        
    }

    public void Update(Scene scene, float deltaTime)
    {
        if (inputHit != "")
        {
            InputHit?.Invoke(scene, inputHit);
            inputHit = "";
        }
    }
    
    public void PublishInputHit(string key) => inputHit = key;
}