using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text score;

    void Start()
    {
        score = GetComponent<Text>();
        GameManager.OnScoreAdd += ChangeScore;
    }

    private void ChangeScore()
    {
        score.text = string.Format("{0:D5}", GameManager.Instance.Score);
    }
}