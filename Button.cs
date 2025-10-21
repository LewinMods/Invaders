using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Button : Entity
{
    public bool isHovered = false;
    private GAMESTATE gameState;
    private string name;

    public bool lastFrameWasHovered = false;

    public Text buttonText;

    public Button(GAMESTATE gameState, string name) : base("button")
    {
        this.gameState = gameState;
        this.name = name;
        
        buttonText = new Text();
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        
        sprite.TextureRect = new IntRect(100, 250, 1436, 1024 - 250);
        sprite.Scale = new Vector2f(0.15f, 0.15f);
        
        buttonText.Font = scene.Assets.LoadFont("ARIAL");
        buttonText.DisplayedString = name;
        
        FloatRect bounds = buttonText.GetLocalBounds();
        buttonText.Origin = new Vector2f(bounds.Width / 2f, bounds.Height / 2f);

        buttonText.Position = Position +  new Vector2f(sprite.GetGlobalBounds().Width / 2f, sprite.GetGlobalBounds().Height / 2f);
        
        sprite.Position += new Vector2f(10, 25);

        scene.Events.InputHit += ButtonPress;
    }

    public override void Render(RenderTarget target)
    {
        base.Render(target);
        
        target.Draw(buttonText);
    }

    private void ButtonPress(Scene scene, string key)
    {
        if (isHovered && key == "Enter" && lastFrameWasHovered == isHovered)
        {
            scene.nextScene = gameState;
        }
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        
        lastFrameWasHovered = isHovered;

        if (isHovered)
        {
            sprite.Color = Color.White;
        }
        else
        {
            sprite.Color = Color.Blue;
        }
    }

    public override void Destroy(Scene scene)
    {
        scene.Events.InputHit -= ButtonPress;
    }
}