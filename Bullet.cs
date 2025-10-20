using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Bullet : Movable
{
    public Actor parent;
    private Vector2f dir;
    private Vector2f pos;
    
    private static Clock clock = new Clock();
    private float time;
    
    public Bullet(Actor parent, Vector2f dir, Vector2f pos) : base("bullet")
    {
        this.parent = parent;
        this.dir = dir;
        this.pos = pos;
        
        time = clock.ElapsedTime.AsSeconds();
    }
    
    public override void Create(Scene scene)
    {
        sprite.TextureRect = new IntRect(0, 0, 40, 69);
        sprite.Scale = new Vector2f(0.5f, 0.5f);

        Direction = dir;
        
        sprite.Rotation = parent.sprite.Rotation;
        
        sprite.Origin = sprite.GetLocalBounds().Size / 2;
        
        Position = pos;
        
        base.Create(scene);
        
        Speed = 500;
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);

        if (clock.ElapsedTime.AsSeconds() > time + 10)
        {
            Dead = true;
        }
    }
}