using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public float score, timer;
    public float kills;
    public Text uiScoreText, uiTimerText, uiHealthText;
    public Text uiPauseText, uiGameOverText;

    public GameObject player, mainMenu;
    public float playerHealth;

    public State currentState;

    public enum State
    {
        MainMenu,
        Playing,
        Paused
    }

    private void Start()
    {
        currentState = State.MainMenu;

        Time.timeScale = 0;

        score = 0;
        timer = 0;
        kills = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = 3f;

        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");

    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        currentState = State.Playing;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == State.MainMenu)
            {
                QuitGame();
            }

            if (currentState == State.Playing)
            {
                Time.timeScale = 0;
                uiPauseText.enabled = true;
                currentState = State.Paused;
            }
            else if (currentState == State.Paused)
            {
                Time.timeScale = 1;
                uiPauseText.enabled = false;
                currentState = State.Playing;
            }
        }

        if (currentState == State.Playing)
        {
            
        }


        if (currentState != State.MainMenu)
        {
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
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame!");
        Application.Quit();
    }

    public void ScorePoints(float points)
    {
        score += points;
    }
}