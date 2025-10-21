using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class Player : Actor
{
    private Clock clock;
    private Clock damageClock;

    private Vector2f gunPosition2;
    
    private bool invulnerable = false;
    private int invulnerableTimer = 500;
    
    public Player() : base("player")
    {
        clock = new Clock();
        damageClock = new Clock();
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
        
        if (Health <= 0)
        {
            scene.EndGame();
            Explode(scene, this);
            Dead = true;
        }
        
        if (invulnerable && damageClock.ElapsedTime.AsMilliseconds() > invulnerableTimer)
        {
            invulnerable = false;
            sprite.Color = Color.White;
        }
        
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
        
        Position = new Vector2f(Math.Clamp(Position.X, 0, Program.ScreenWidth - WorldHitbox.Width - 36), Math.Clamp(Position.Y, 0, Program.ScreenHeight - WorldHitbox.Height - 36));
    }

    private void Shoot(Scene scene, string key)
    {
        if (key == "Space" && clock.ElapsedTime.AsMilliseconds() >= ShootCooldown)
        {
            GunPosition = Position + new Vector2f(sprite.GetGlobalBounds().Size.X / 4, 0);
            gunPosition2 = Position + new Vector2f(sprite.GetGlobalBounds().Size.X / 1.25f, 0);
            
            scene.Spawn(new Bullet(this, new Vector2f(0, -1), GunPosition));
            scene.Spawn(new Bullet(this, new Vector2f(0, -1), gunPosition2));
            
            RestartShootTimer();
        }
    }
    
    private void RestartShootTimer()
    {
        clock.Restart();
        ShootCooldown = 500;
    }
    
    protected override FloatRect LocalHitbox => new FloatRect(50, 50, 170, 140);

    protected override void CollideWith(Scene scene, Entity other)
    {
        if (other is Explosion) return;
        if (invulnerable) return;
        
        base.CollideWith(scene, other);
        
        sprite.Color = Color.Red;
        invulnerable = true;
        damageClock.Restart();
    }

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        
        scene.Events.InputHit -= Shoot;
    }
}