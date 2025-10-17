using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Actor : Movable
{
    private Vector2f originalPosition;
    protected float ShootCooldown;
    public int Health;
    
    protected Actor(string textureName) : base(textureName)
    {
        
    }

    protected void Reset()
    {
        Position = originalPosition;
    }

    public override void Create(Scene scene)
    {
        originalPosition = Position;
        
        base.Create(scene);
    }
}