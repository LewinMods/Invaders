using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Bullet : Movable
{
    public Actor parent;
    private Vector2f dir;
    
    public Bullet(Actor parent, Vector2f dir) : base("bullet")
    {
        this.parent = parent;
        this.dir = dir;
    }
    
    public override void Create(Scene scene)
    {
        sprite.TextureRect = new IntRect(0, 0, 40, 69);
        sprite.Scale = new Vector2f(0.5f, 0.5f);

        Direction = dir;
        
        sprite.Rotation = parent.sprite.Rotation;
        
        sprite.Origin = sprite.GetLocalBounds().Size / 2;
        
        Position = parent.GunPosition;
        
        base.Create(scene);
        
        Speed = 500;
    }
}