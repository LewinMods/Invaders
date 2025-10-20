using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class DeathMenuGUI : GUI
{
    public DeathMenuGUI() : base("button")
    {
        title = "You Died";
    }

    public override void Create(Scene scene)
    {
        buttonAmount = 2;
        
        base.Create(scene);
        
        buttons[0] = new Button(GAMESTATE.GAMESCREEN, "Play Again") {Position = new Vector2f(0, 400)};
        buttons[1] = new Button(GAMESTATE.MAINMENU, "Main Menu") {Position = new Vector2f(0, 600)};
        
        CreateButtons(scene);
    }
}