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
                scene.StartGame();
                scene.Spawn(new GameGUI());
                break;
            
            case GAMESTATE.HIGHSCOREMENU: 
                scene.Spawn(new HighscoreMenuGUI());
                break;
            
            case GAMESTATE.DEATHMENU:
                scene.clock = null;
                scene.Spawn(new DeathMenuGUI());
                break;
        }
    }
}