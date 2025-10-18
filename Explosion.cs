using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Explosion : Entity
{
    private static List<IntRect> explosionFrames = new List<IntRect>()
    {
        new IntRect(0,     0, 234, 347),
        new IntRect(296,   0, 323, 347),
        new IntRect(381 + 296,   0, 323, 347),
        new IntRect(383 + 381 + 296,  0, 318, 347),
    };
    
    Animation anim;
        
    public Explosion() : base("explosion1")
    {
        ZIndex = 2;

        anim = new Animation(explosionFrames);
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        Position -= new Vector2f(323 / 2, 347 / 2);
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);

        if (sprite.TextureRect == new IntRect(383 + 381 + 296, 0, 318, 347))
        {
            Dead = true;
        }
        
        sprite.TextureRect = anim.UpdateAnimation();
    }
}