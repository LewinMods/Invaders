using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class DeathMenuGUI : GUI
{
    private Text text;
    private Text Answertext;

    private string answerText = "";
    private string acceptableKey = "abcdefghijklmnopqrstuvwxyz";
    
    public DeathMenuGUI() : base("button")
    {
        title = "You Died";
        text = new Text();
        Answertext = new Text();
    }

    public override void Create(Scene scene)
    {
        buttonAmount = 2;
        
        base.Create(scene);
        scene.Events.InputHit -= CycleButtons;
        
        buttons[0] = new Button(GAMESTATE.GAMESCREEN, "Play Again") {Position = new Vector2f(0, 400)};
        buttons[1] = new Button(GAMESTATE.MAINMENU, "Main Menu") {Position = new Vector2f(0, 600)};
        
        text.Font = scene.Assets.LoadFont("ARIAL");
        text.DisplayedString = "Enter your name (5 letters max)";
        text.Position = new Vector2f((Program.ScreenWidth - text.GetGlobalBounds().Width) / 2, 200 - text.GetGlobalBounds().Height * 2f);
        
        Answertext.Font = scene.Assets.LoadFont("ARIAL");
        Answertext.DisplayedString = answerText;
        Answertext.Position = new Vector2f((Program.ScreenWidth - text.GetGlobalBounds().Width) / 2, 250 - text.GetGlobalBounds().Height * 2f);
        
        scene.Events.InputHit += Writing;
        
        CreateButtons(scene);
        buttons[0].isHovered = false;
    }

    public override void Render(RenderTarget target)
    {
        base.Render(target);
        
        target.Draw(text);
        target.Draw(Answertext);
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);

        Answertext.DisplayedString = answerText;
    }

    private void Writing(Scene scene, string key)
    {
        if (key == "Enter" && answerText.Length > 0)
        {
            scene.Events.InputHit -= Writing;
            scene.Events.InputHit += CycleButtons;
            buttons[0].isHovered = true;
        }
        
        else if (key == "BackSpace" && answerText.Length > 0)
        {
            answerText = answerText.Substring(0, answerText.Length - 1);
        }
        
        else if (acceptableKey.Contains(key.ToLower()) && answerText.Length < 5)
        {
            answerText += key;
        }
    }
}