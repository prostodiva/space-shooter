using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }
    void Start()
    {
        if (scoreText == null)
        {
            return;
        }

        if (scoreKeeper == null)
        {
            scoreText.text = "You Scored:\n0";
            return;
        }
        
        scoreText.text = "You Scored:\n" + scoreKeeper.GetScore();
    }
}
