using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Actor : Movable
{
    private Vector2f originalPosition;
    protected float ShootCooldown;
    public int Health;
    
    protected IntRect stillFrame;
    protected IntRect movingFrame;
    
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

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);

        if (Direction != new Vector2f(0, 0))
        {
            sprite.TextureRect = movingFrame;
        }
        else
        {
            sprite.TextureRect = stillFrame;
        }
    }
}