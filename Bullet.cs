using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Bullet : Movable
{
    private Actor parent;
    private Vector2f dir;
    
    public Bullet(Actor parent, Vector2f dir) : base("spaceship")
    {
        this.parent = parent;
        this.dir = dir;
    }
    
    public override void Create(Scene scene)
    {
        sprite.TextureRect = new IntRect(55, 40, 150, 220);
        sprite.Scale = new Vector2f(0.1f, 0.1f);
        sprite.Color = Color.Yellow;
        
        Position = parent.GunPosition - new Vector2f((sprite.GetGlobalBounds().Size.X) / 2, 0);

        Direction = dir;
        
        base.Create(scene);
        
        Speed = 500;
    }
}