using UnityEngine;
using TMPro;
using Core;

public class GameOverUpdateUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private void OnEnable()
    {
        scoreText.text = "Score: " + GameManager.Instance.score;
        highscoreText.text = "Highscore: " + GameManager.Instance.highScore;
    }
}
