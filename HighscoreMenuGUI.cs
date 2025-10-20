using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class HighscoreMenuGUI : GUI
{
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
    }
}