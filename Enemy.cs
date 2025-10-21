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

        int x = new Random().Next((int)MathF.Ceiling(sprite.Origin.X), (int)MathF.Floor(Program.ScreenWidth - sprite.Origin.X));
        int y = new Random().Next(-700, (int)MathF.Ceiling(-sprite.Origin.Y));
        
        Position = new Vector2f(x, y);
        
        base.Create(scene);
        
        Speed = 200;
        Health = 1;

        PickDirection();
        RestartShootTimer();
    }

    public override void Update(Scene scene, float deltaTime)
    {
        if (Health <= 0)
        {
            Explode(scene, this);
            Dead = true;
        }
        
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
        
        scene.Spawn(new Bullet(this, Direction, GunPosition));
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
        if (other is Explosion) return;
        if (other is Enemy) return;
        if (other is Bullet bullet && bullet.parent is Enemy) return;
        
        base.CollideWith(scene, other);
    }
}