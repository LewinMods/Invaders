using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Explosion : Entity
{
    private static List<IntRect> explosionFrames = new List<IntRect>()
    {
        new IntRect(0,     0, 323, 347),
        new IntRect(296,   0, 323, 347),
        new IntRect(381 + 296,   0, 323, 347),
        new IntRect(383 + 381 + 296,  0, 323, 347),
    };
    
    Animation anim;
        
    public Explosion() : base("explosion1")
    {
        ZIndex = 2;

        anim = new Animation(explosionFrames, true);
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);

        sprite.TextureRect = new IntRect(0, 0, 323, 347);
        sprite.Scale = new Vector2f(0.5f, 0.5f);
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        
        sprite.TextureRect = anim.UpdateAnimation();

        if (sprite.TextureRect == new IntRect())
        {
            Dead = true;
        }
    }
}