using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public float score, timer;
    public float kills;
    public Text uiScoreText, uiTimerText, uiHealthText;

    public GameObject player, mainMenu, pauseMenu, gameOverMenu;
    public float playerHealth;

    public State currentState;

    public enum State
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    private float startingHealth = 3f;

    private void Start()
    {
        currentState = State.MainMenu;

        Time.timeScale = 0;

        score = 0;
        timer = 0;
        kills = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = startingHealth;

        if (mainMenu == null)
            mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
    }

    public void StartGame()
    {
        PlayGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == State.MainMenu)
            {
                QuitGame();
            }
            else if (currentState == State.Playing)
            {
                PauseGame();
            }
            else if (currentState == State.Paused)
            {
                PlayGame();
            }
            else if (currentState == State.GameOver)
            {
                QuitToMainMenu();
            }
        }

        if (currentState == State.Paused)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                QuitToMainMenu();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                PlayGame();
            }
        }

        if (currentState == State.Playing)
        {
            if (playerHealth == 0)
            {
                currentState = State.GameOver;
                Time.timeScale = 0;
                gameOverMenu.SetActive(true);
            }

            timer += Time.deltaTime;

            uiScoreText.text = "Score: " + score.ToString();
            uiTimerText.text = "Time: " + Mathf.Floor(timer).ToString();
            uiHealthText.text = "Health: " + playerHealth.ToString();
        }
    }

    private void PauseGame()
    {
        Debug.Log("PauseGame!");
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        currentState = State.Paused;
    }

    public void PlayGame()
    {
        Debug.Log("PlayGame!");
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        currentState = State.Playing;
        Time.timeScale = 1;

        Vector3 pos = player.transform.position;
        pos.y = -4.3f;
        player.transform.position = pos;
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame!");
        Application.Quit();
    }

    public void QuitToMainMenu()
    {
        Debug.Log("QuitToMainMenu!");
        ResetGame();
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        currentState = State.MainMenu;
        Time.timeScale = 0;
    }

    private void ResetGame()
    {
        player.transform.position = new Vector3(0, -5.6f, 0);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject e in enemies)
        {
            Destroy(e);
        }
        foreach (GameObject p in projectiles)
        {
            Destroy(p);
        }

        score = 0;
        timer = 0;
        kills = 0;

        playerHealth = startingHealth;
    }

    public void ScorePoints(float points)
    {
        score += points;
    }
}