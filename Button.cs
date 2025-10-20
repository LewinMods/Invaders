using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Button : Entity
{
    public bool isHovered = false;
    private GAMESTATE gameState;
    private string name;

    private Text buttonText;

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
        
        buttonText.Position = Position + new Vector2f(Program.ScreenWidth / 2, 0) - buttonText.GetLocalBounds().Size/2;

        scene.Events.InputHit += ButtonPress;
    }

    public override void Render(RenderTarget target)
    {
        base.Render(target);
        
        target.Draw(buttonText);
    }

    private void ButtonPress(Scene scene, string key)
    {
        if (isHovered && key == "Enter")
        {
            SceneLoader.InitiateScene(scene, gameState);
        }
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);

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