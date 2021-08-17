using Core;

[System.Serializable]
public class PlayerData
{
    public int highScore;
    public bool tutorialSeen;

    public PlayerData (GameManager gameManager)
    {
        highScore = gameManager.highScore;
        tutorialSeen = gameManager.tutorialSeen;
    }
}
