using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public sealed class Scene
{
    private List<Entity> entities;
    
    public readonly AssetManager Assets;
    public readonly EventManager Events;
    public readonly SaveFile SaveFile;
    public readonly InputManager Inputs;

    public Clock? clock;
    private int bufferTime = 2000;
    private float scoreTime = 0;
    
    public int Score;
    
    public GAMESTATE? nextScene = null;

    public Scene()
    {
        entities = new List<Entity>();
        
        Assets = new AssetManager();
        Events = new EventManager();
        SaveFile = new SaveFile("SaveFile");
        Inputs = new InputManager(new List<string>(){"Space", "Enter", "W", "S"});
        
        nextScene = GAMESTATE.MAINMENU;
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
        
        entities = entities.OrderBy(e => e.ZIndex).ToList();
    }

    public void UpdateAll(float deltaTime)
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            entities[i].Update(this, deltaTime);
        }
        
        Inputs.Update(this);
        Events.Update(this, deltaTime);
        
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            if (entities[i].Dead)
            {
                Entity entity = entities[i];
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
        
        if (clock != null)
        {
            scoreTime += deltaTime;
            
            if (scoreTime >= 1)
            {
                Score += 1;
                scoreTime -= 1;
            }
            
            if (clock.ElapsedTime.AsMilliseconds() > bufferTime)
            {
                clock.Restart();
                Spawn(new Enemy());
                
                bufferTime = Math.Clamp((int)MathF.Floor(bufferTime * 0.98f), 300, 1000);
            }
        }
        
        if (nextScene.HasValue)
        {
            SceneLoader.InitiateScene(this, nextScene.Value);
            nextScene = null;
        }
    }

    public void RenderAll(RenderTarget target)
    {
        foreach (Entity entity in entities)
        {
            entity.Render(target);
        }
    }

    public bool FindByType<T>(out T found) where T : Entity
    {
        foreach (Entity entity in entities)
        {
            if (!entity.Dead && entity is T typed)
            {
                found = typed;
                return true;
            }
        }
        
        found = default(T);
        return false;
    }

    public void Clear()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            
            entities.RemoveAt(i);
            entity.Destroy(this);
        }
    }

    public IEnumerable<Entity> FindIntersects(FloatRect bounds)
    {
        int lastEntity = entities.Count - 1;

        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            
            if (entity.Dead) continue;
            if (entity.WorldHitbox == bounds) continue;
            
            if (entity.WorldHitbox.Intersects(bounds))
            {
                yield return entity;
            }
        }
    }

    public void StartGame()
    {
        bufferTime = 2000;
        
        Spawn(new Player());
        Spawn(new Enemy());

        clock = new Clock();
    }

    public void EndGame()
    {
        nextScene = GAMESTATE.DEATHMENU;
        Console.Clear();
    }
}