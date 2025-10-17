using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class Player : Actor
{
    public Player() : base("spaceship")
    {
        
    }

    public override void Create(Scene scene)
    {
        sprite.TextureRect = new IntRect(55, 40, 150, 220);
        sprite.Scale = new Vector2f(0.7f, 0.7f);
        
        Position = new Vector2f((Program.ScreenWidth - sprite.GetGlobalBounds().Size.X) / 2, Program.ScreenHeight - (sprite.GetGlobalBounds().Size.Y) * 1.1f);
        
        base.Create(scene);
        
        Speed = 200;
        Health = 5;

        scene.Events.InputHit += Shoot;
    }

    public override void Update(Scene scene, float deltaTime)
    {
        Direction = new Vector2f(0, 0);
        
        if (Keyboard.IsKeyPressed(Keyboard.Key.A))
        {
            Direction += new Vector2f(-1, 0);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
        {
            Direction += new Vector2f(1, 0);
        }
        
        if (Position.X >= Program.ScreenWidth - sprite.GetGlobalBounds().Size.X)
        {
            Direction += new Vector2f(-1, 0);
        }
        if (Position.X <= 0)
        {
            Direction += new Vector2f(1, 0);
        }
        
        base.Update(scene, deltaTime);
    }

    private void Shoot(Scene scene, string key)
    {
        if (key == "Space")
        {
            GunPosition = Position + new Vector2f(sprite.GetGlobalBounds().Size.X / 2, 0);
            
            scene.Spawn(new Bullet(this, new Vector2f(0, -1)));
        }
    }
}