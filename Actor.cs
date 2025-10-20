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
        
        foreach (Entity found in scene.FindIntersects(WorldHitbox))
        {
            CollideWith(scene, found);
        }

        if (Direction != new Vector2f(0, 0))
        {
            sprite.TextureRect = movingFrame;
        }
        else
        {
            sprite.TextureRect = stillFrame;
        }
    }
    
    protected override void CollideWith(Scene scene, Entity other)
    {
        if (other is Bullet bullet && bullet.parent != this)
        {
            bullet.Dead = true;
            Health -= 1;
        }

        else if (other is Player player || other is Enemy enemy)
        {
            Health -= 1;
        }
        
        if (Health <= 0)
        {
            Explode(scene, this);
        }
    }

    private void Explode(Scene scene, Actor hit)
    {
        scene.Spawn(new Explosion() {Position = hit.Position - new Vector2f(380 / 4, 347 / 4)});
        
        Dead = true;
    }
}