using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class MainMenuGUI : GUI
{
    public MainMenuGUI() : base("button")
    {
        title = "Invaders";
    }

    public override void Create(Scene scene)
    {
        buttonAmount = 3;
        
        base.Create(scene);
        
        buttons[0] = new Button(GAMESTATE.GAMESCREEN, "Play") {Position = new Vector2f(0, 200)};
        buttons[1] = new Button(GAMESTATE.HIGHSCOREMENU, "Highscores") {Position = new Vector2f(0, 400)};
        buttons[2] = new Button(GAMESTATE.CLOSE, "Quit") {Position = new Vector2f(0, 600)};
        
        CreateButtons(scene);
    }
}