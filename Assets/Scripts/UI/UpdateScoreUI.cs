using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core;

public class UpdateScoreUI : MonoBehaviour
{
    private static TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        scoreText.text = FindObjectOfType<GameManager>().score.ToString();
    }

    public static void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
