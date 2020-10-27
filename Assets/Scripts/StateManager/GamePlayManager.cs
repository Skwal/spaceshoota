using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public Text uiScoreText, uiTimerText, uiHealthText;
    private GameState gameState;

    private void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();
        Debug.Log(gameState);

        gameState.PlayGame();
    }

    private void Update()
    {
        if (uiScoreText != null && gameState.score != 0)
            uiScoreText.text = "SCORE: " + gameState.score.ToString();
        if (uiTimerText != null && gameState.timer != 0)
            uiTimerText.text = "TIME: " + Mathf.Floor(gameState.timer).ToString();
        if (uiHealthText != null && gameState.playerHealth != null)
            uiHealthText.text = "HULL: " + gameState.playerHealth.currentHealth.ToString();
    }
}