using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class HighscoreMenuGUI : GUI
{
    List<Text> scoreText = new List<Text>();
    
    public HighscoreMenuGUI() : base("button")
    {
        title = "Highscores";
    }

    public override void Create(Scene scene)
    {
        buttonAmount = 1;
        
        base.Create(scene);
        
        buttons[0] = new Button(GAMESTATE.MAINMENU, "Back") {Position = new Vector2f(0, 700)};
        
        CreateButtons(scene);

        var scores = scene.SaveFile.LoadAll();

        for (int i = 0; i < scores.Count; i++)
        {
            Text text = new Text();
            scoreText.Add(text);
            
            text.Font = scene.Assets.LoadFont("ARIAL");
            text.DisplayedString = scores[i].Name + ": " + scores[i].Score;
            text.Position = new Vector2f(Program.ScreenWidth / 2 - text.GetGlobalBounds().Width / 2, 150 + 40 * i);
        }
    }

    public override void Render(RenderTarget target)
    {
        base.Render(target);

        foreach (Text text in scoreText)
        {
            target.Draw(text);
        }
    }
}