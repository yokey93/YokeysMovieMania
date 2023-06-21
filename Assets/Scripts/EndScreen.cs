using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText;
    Score score;

    void Awake()
    {
        score = FindAnyObjectByType<Score>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations! Final Score = " + score.CalculateScore() + "%";
    }

}
