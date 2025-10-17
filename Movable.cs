using SFML.System;
using SFML.Window;

namespace Invaders;

public abstract class Movable: Entity
{
    protected float Speed;
    protected Vector2f Direction;

    public Vector2f GunPosition;
    
    protected Movable(string textureName) : base(textureName)
    {
        
    }

    public override void Update(Scene scene, float deltaTime)
    {
        if (Direction.X != 0 || Direction.Y != 0)
        {
            float length = (float)Math.Sqrt(Direction.X * Direction.X + Direction.Y * Direction.Y);
            Direction /= length;
        }
        
        Position += Direction * Speed * deltaTime;
    }
}