using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public float timer;

    // stats
    public int enemyKilled, objectsDestroyed, score, money;

    public GameObject player, pauseMenu, gameOverMenu;

    public State currentState;

    public enum State
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    public Health playerHealth;

    private void Awake()
    {
        GameObject[] multiGS = GameObject.FindGameObjectsWithTag("GameState");

        if (multiGS.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        currentState = State.MainMenu;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentState)
            {
                case State.MainMenu:
                    QuitGame();
                    break;

                case State.Playing:
                    PauseGame();
                    break;

                case State.Paused:
                    UnpauseGame();
                    break;

                case State.GameOver:
                    QuitToMainMenu();
                    break;
            }
        }

        if (currentState == State.Paused)
        {
            if (Input.GetKeyDown(KeyCode.Y))
                QuitToMainMenu();
            else if (Input.GetKeyDown(KeyCode.N))
                UnpauseGame();
        }

        if (currentState == State.Playing)
        {
            timer += Time.deltaTime;
        }

        if (currentState == State.GameOver && Input.GetButton("Fire1"))
            QuitToMainMenu();
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    private void LateUpdate()
    {
        if (currentState == State.Playing)
        {
            if (playerHealth.currentHealth == 0)
            {
                currentState = State.GameOver;
                StartCoroutine(ShowGameOverAfterDelay(1f));
            }
        }
    }

    private void PauseGame()
    {
        Debug.Log("PauseGame!");
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        currentState = State.Paused;
    }

    private void UnpauseGame()
    {
        Debug.Log("UnpauseGame!");
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        currentState = State.Playing;
    }

    public void StartNewGame()
    {
        Debug.Log("StartNewGame!");
        SceneManager.LoadScene("Level01");

        score = 0;
        timer = 0;
        enemyKilled = 0;
        objectsDestroyed = 0;
        money = 0;
    }

    public void PlayGame()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();

        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
        gameOverMenu = GameObject.FindGameObjectWithTag("GameOverMenu");
        gameOverMenu.SetActive(false);

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
        currentState = State.MainMenu;
        Time.timeScale = 0;

        // needed?
        pauseMenu = null;
        gameOverMenu = null;
        player = null;
        playerHealth = null;

        SceneManager.LoadScene("MainMenu");
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
        enemyKilled = 0;
        objectsDestroyed = 0;
        money = 0;

        playerHealth.RecoverHealth(playerHealth.maxHealth);
    }

    public void ScorePoints(int points)
    {
        score += points;
    }
}