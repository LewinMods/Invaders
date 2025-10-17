using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class Enemy : Actor
{
    private Clock clock;
    
    public Enemy() : base("spaceship")
    {
        clock = new Clock();
    }

    public override void Create(Scene scene)
    {
        sprite.TextureRect = new IntRect(55, 40, 150, 220);
        sprite.Scale = new Vector2f(0.7f, 0.7f);
        sprite.Color = Color.Red;
        
        Position = new Vector2f((Program.ScreenWidth - sprite.GetGlobalBounds().Size.X) / 2, -sprite.GetGlobalBounds().Size.Y);
        
        base.Create(scene);
        
        Speed = 200;
        Health = 5;

        PickDirection();
        RestartShootTimer();
    }

    public override void Update(Scene scene, float deltaTime)
    {
        if (clock.ElapsedTime.AsMilliseconds() >= ShootCooldown)
        {
            Shoot(scene);
        }
        
        if (Position.X >= Program.ScreenWidth - sprite.GetGlobalBounds().Size.X)
        {
            Direction = new Vector2f(-1, 1);
        }
        if (Position.X <= 0)
        {
            Direction = new Vector2f(1, 1);
        }
        
        if (Position.Y >= Program.ScreenHeight)
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
    }
    
    private void Shoot(Scene scene)
    {
        GunPosition = Position + new Vector2f(sprite.GetGlobalBounds().Size.X / 2, sprite.GetGlobalBounds().Size.Y);
        
        scene.Spawn(new Bullet(this, Direction));
        RestartShootTimer();
    }

    private void RestartShootTimer()
    {
        clock.Restart();
        ShootCooldown = new Random().Next(500, 2001);
    }
}