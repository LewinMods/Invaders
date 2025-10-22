using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Entity
{
    private string textureName;
    public Sprite sprite;
    public bool Dead = false;
    public int ZIndex = 0;

    protected Entity(string textureName)
    {
        this.textureName = textureName;
        sprite = new Sprite();
    }

    public Vector2f Position
    {
        get => sprite.Position;
        set => sprite.Position = value;
    }

    public virtual void Create(Scene scene)
    {
        sprite.Texture = scene.Assets.LoadTexture(textureName);
    }

    public virtual void Update(Scene scene, float deltaTime)
    {
        
    }

    public virtual void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }

    protected virtual void CollideWith(Scene s, Entity other)
    {
        
    }

    public virtual void Destroy(Scene scene)
    {
        
    }
    
    public FloatRect WorldHitbox
    {
        get
        {
            var transform = sprite.Transform;
            return transform.TransformRect(LocalHitbox);
        }
    }

    protected virtual FloatRect LocalHitbox => new FloatRect(0, 0, sprite.TextureRect.Width, sprite.TextureRect.Height);
}