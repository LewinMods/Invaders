namespace Invaders;

public delegate void InputEvent(Scene scene, string key);
public delegate void ChangeValueFromActorEvent(Actor actor);

public class EventManager
{
    public event InputEvent InputHit;
    public event ChangeValueFromActorEvent TakeDamage;

    private string inputHit;
    private List<Actor> actor;
    
    public EventManager()
    {
        actor =  new List<Actor>();
    }

    public void Update(Scene scene, float deltaTime)
    {
        if (inputHit != "")
        {
            InputHit?.Invoke(scene, inputHit);
            inputHit = "";
        }
        
        foreach (Actor a in actor) 
        { 
            TakeDamage?.Invoke(a);
        }
        
        actor.Clear();
    }
    
    public void PublishInputHit(string key) => inputHit = key;
    public void PublishTakeDamage(Actor actor) => this.actor.Add(actor);
}