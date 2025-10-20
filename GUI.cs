using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class GUI : Entity
{
    private Text titleText;
    protected string title;

    protected int buttonAmount;
    
    protected Button[] buttons;
    
    protected GUI(string textureName) : base(textureName)
    {
        titleText = new Text();
    }

    public override void Create(Scene scene)
    {
        buttons = new Button[buttonAmount];
        titleText.Font = scene.Assets.LoadFont("ARIAL");
        titleText.DisplayedString = title;
        titleText.Position = new Vector2f((Program.ScreenWidth - titleText.GetGlobalBounds().Width) / 2, titleText.GetGlobalBounds().Height * 2f);
        
        scene.Events.InputHit += CycleButtons;
    }

    public override void Render(RenderTarget target)
    {
        base.Render(target);
        
        target.Draw(titleText);
    }
    
    protected void CreateButtons(Scene scene)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            scene.Spawn(buttons[i]);
            buttons[i].Position += new Vector2f((Program.ScreenWidth - buttons[i].sprite.GetGlobalBounds().Size.X) / 2, 0);
        }

        if (buttons.Length > 0)
        {
            buttons[0].isHovered = true;
        }
    }

    private void CycleButtons(Scene scene, string key)
    {
        switch (key)
        {
            case "S":
                for (int i = buttons.Length - 1; i >= 0; i--)
                {
                    if (buttons[i].isHovered)
                    {
                        buttons[i].isHovered = false;
                        
                        if (i == buttons.Length - 1)
                        {
                            buttons[0].isHovered = true;
                            break;
                        }
                        
                        buttons[i + 1].isHovered = true;
                    }
                }
                break;
            
            case "W":
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].isHovered)
                    {
                        buttons[i].isHovered = false;
                        
                        if (i == 0)
                        {
                            buttons[buttons.Length - 1].isHovered = true;
                            break;
                        }
                        
                        buttons[i - 1].isHovered = true;
                    }
                }
                break;
        }
    }

    public override void Destroy(Scene scene)
    {
        scene.Events.InputHit -= CycleButtons;
    }
}