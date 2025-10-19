using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class Enemy : Actor
{
    private Clock clock;
    
    public Enemy() : base("enemy")
    {
        clock = new Clock();
        ZIndex = 1;
    }

    public override void Create(Scene scene)
    {
        movingFrame = new IntRect(0, 0, 130, 250);
        sprite.TextureRect = movingFrame;
        
        sprite.Scale = new Vector2f(0.7f, 0.7f);
        sprite.Origin = sprite.GetLocalBounds().Size / 2;
        
        Position = new Vector2f(Program.ScreenWidth / 2 - sprite.Origin.X, -sprite.Origin.Y);
        
        base.Create(scene);
        
        Speed = 200;
        Health = 1;

        PickDirection();
        RestartShootTimer();
    }

    public override void Update(Scene scene, float deltaTime)
    {
        if (Position.X >= Program.ScreenWidth - sprite.Origin.X)
        {
            Direction = new Vector2f(-1, 1);
            RotateTowardsDirection();
        }
        if (Position.X <= sprite.Origin.X)
        {
            Direction = new Vector2f(1, 1);
            RotateTowardsDirection();
        }
        
        if (clock.ElapsedTime.AsMilliseconds() >= ShootCooldown)
        {
            Shoot(scene);
        }
        
        if (Position.Y >= Program.ScreenHeight + WorldHitbox.Height)
        {
            Reset();
        }
        
        base.Update(scene, deltaTime);
    }

    private void PickDirection()
    {
        int rand = new Random().Next(0, 2);

        switch (rand)
        {
            case 0:
                Direction = new Vector2f(1, 1);
                break;
            case 1:
                Direction = new Vector2f(-1, 1);
                break;
        }

        RotateTowardsDirection();
    }
    
    private void Shoot(Scene scene)
    {
        GunPosition = Position;
        
        scene.Spawn(new Bullet(this, Direction));
        RestartShootTimer();
    }

    private void RestartShootTimer()
    {
        clock.Restart();
        ShootCooldown = new Random().Next(500, 2001);
    }

    private void RotateTowardsDirection()
    {
        if (Direction == (1, 1))
        {
            sprite.Rotation = 135;
        }
        else
        {
            sprite.Rotation = 225;
        }
    }
    
    protected override FloatRect LocalHitbox => new FloatRect(45, 0, 45, 150);

    protected override void CollideWith(Scene scene, Entity other)
    {
        if (other is Enemy) return;
        
        base.CollideWith(scene, other);
    }
}