using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class Player : Actor
{
    private Clock clock;
    
    public Player() : base("player")
    {
        clock = new Clock();
        ZIndex = 1;
    }

    public override void Create(Scene scene)
    {
        stillFrame = new IntRect(0, 0, 200, 190);
        movingFrame = new IntRect(199, 0, 200, 250);
        sprite.TextureRect = stillFrame;
        
        sprite.Scale = new Vector2f(0.7f, 0.7f);
        
        Position = new Vector2f((Program.ScreenWidth - sprite.GetGlobalBounds().Size.X) / 2, Program.ScreenHeight - (sprite.GetGlobalBounds().Size.Y * 1.1f));
        
        base.Create(scene);
        
        Speed = 200;
        Health = 5;

        scene.Events.InputHit += Shoot;
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        
        Direction = new Vector2f(0, 0);
        
        if (Keyboard.IsKeyPressed(Keyboard.Key.W))
        {
            Direction += new Vector2f(0, -1);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.S))
        {
            Direction += new Vector2f(0, 1);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.A))
        {
            Direction += new Vector2f(-1, 0);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
        {
            Direction += new Vector2f(1, 0);
        }
        
        Position = new Vector2f(Math.Clamp(Position.X, 0, Program.ScreenWidth - Bounds.Width), Math.Clamp(Position.Y, 0, Program.ScreenHeight - Bounds.Height));
    }

    private void Shoot(Scene scene, string key)
    {
        if (key == "Space" && clock.ElapsedTime.AsMilliseconds() >= ShootCooldown)
        {
            GunPosition = Position + new Vector2f(sprite.GetGlobalBounds().Size.X / 2, 0);
            
            scene.Spawn(new Bullet(this, new Vector2f(0, -1)));
            
            RestartShootTimer();
        }
    }
    
    private void RestartShootTimer()
    {
        clock.Restart();
        ShootCooldown = 500;
    }
    
    public override FloatRect Bounds
    {
        get
        {
            var bounds = base.Bounds;
            bounds.Width = 140;
            bounds.Height = 132;
            return bounds;
        }
    }
}