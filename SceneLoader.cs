namespace Invaders;

public static class SceneLoader
{
    public static void InitiateScene(Scene scene, GAMESTATE state)
    {
        scene.Clear();
        
        switch (state)
        {
            case GAMESTATE.MAINMENU:
                scene.Spawn(new MainMenuGUI());
                break;
            
            case GAMESTATE.GAMESCREEN:
                scene.Spawn(new Player());
                scene.Spawn(new Enemy());
                break;
            
            case GAMESTATE.HIGHSCOREMENU: 
                scene.Spawn(new HighscoreMenuGUI());
                break;
            
            case GAMESTATE.DEATHMENU:
                break;
        }
    }
}