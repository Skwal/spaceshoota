using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public float score, timer;
    public Text UIScoreText, UITimerText;

    void Start()
    {
        score = 0;
        timer = 0;
    }


    void Update()
    {
        timer += Time.deltaTime;

        UIScoreText.text = "Score: " + score.ToString();
        UITimerText.text = "Time: " + Mathf.Floor(timer).ToString();
    }

    public void ScorePoints(float points)
    {
        score += points;
    }
}
