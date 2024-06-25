using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScoreController : MonoBehaviour
{
    Text scoreText;
    int score;
    public int Score { get { return this.score; } set { this.score = value; UpdateScoreText(); } }

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    public void UpdateScoreText()
    {
        scoreText.text = $"{score}";
    }
}
