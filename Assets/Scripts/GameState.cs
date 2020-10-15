using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public float score, timer;
    public float kills;
    public Text uiScoreText, uiTimerText, uiHealthText;
    public Text uiPauseText, uiGameOverText;

    public GameObject player;
    public float playerHealth;

    private void Start()
    {
        score = 0;
        timer = 0;
        kills = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = 3f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                uiPauseText.enabled = true;
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                uiPauseText.enabled = false;
            }
        }

        if (playerHealth == 0)
        {
            Time.timeScale = 0;
            uiGameOverText.enabled = true;
        }

        timer += Time.deltaTime;

        uiScoreText.text = "Score: " + score.ToString();
        uiTimerText.text = "Time: " + Mathf.Floor(timer).ToString();
        uiHealthText.text = "Health: " + playerHealth.ToString();
    }

    public void ScorePoints(float points)
    {
        score += points;
    }
}