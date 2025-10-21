using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class GameGUI : Entity
{
    private Text scoreLabel;

    private int health = 5;
    
    public GameGUI() : base("player")
    {
        scoreLabel = new Text();
        ZIndex = 5;
    }
    
    public override void Create(Scene scene)
    {
        sprite.TextureRect = new IntRect(0, 0, 200, 190);
        sprite.Scale = new Vector2f(0.1f, 0.1f);
        
        scoreLabel.Font = scene.Assets.LoadFont("ARIAL");
        scoreLabel.DisplayedString = $"Score: {scene.Score}";
        
        scoreLabel.Position = new Vector2f(36, 80);
        
        base.Create(scene);
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        
        scoreLabel.DisplayedString = $"Score: {scene.Score}";

        if (scene.FindByType<Player>(out Player player))
        {
            health = player.Health;
        }
        else
        {
            health = 0;
        }
    }

    public override void Render(RenderTarget target)
    {
        sprite.Position = new Vector2f(36, 36);

        for (int i = 0; i < health; i++)
        {
            base.Render(target);

            sprite.Position += new Vector2f(36, 0);
        }
        
        target.Draw(scoreLabel);
    }
}