﻿using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    private GameState gameState;

    private int previousHealth, previousShields, previousMissiles;

    public GameObject healthBar, shieldsBar, missilesBar, player;

    Health playerHealth, shieldsHealth;
    int numMissiles, maxMissiles;

    private void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();
        player = GameObject.FindGameObjectWithTag("Player");

        playerHealth = player.GetComponent<Health>();
        shieldsHealth = player.transform.GetChild(4).GetComponent<Health>();

        numMissiles = player.GetComponent<PlayerController>().numMissiles;
        maxMissiles = player.GetComponent<PlayerController>().maxMissiles;

        gameState.PlayGame();

        DisplayHealthBar(); 
        DisplayShieldsBar();
        DisplayMissilesBar();
    }

    private void DisplayHealthBar()
    {
        DisplayBar(healthBar, playerHealth.maxHealth, playerHealth.currentHealth);

        previousHealth = playerHealth.currentHealth;
    }

    private void DisplayShieldsBar()
    {
        DisplayBar(shieldsBar, shieldsHealth.maxHealth, shieldsHealth.currentHealth);

        previousShields = shieldsHealth.currentHealth;
    }

    private void DisplayMissilesBar()
    {
        DisplayBar(missilesBar, maxMissiles * 10, numMissiles * 10);

        previousShields = numMissiles;
    }

    private void DisplayBar(GameObject bar, int max, int current)
    {
        Image barBg, barBgRight;
        RectMask2D currentBar;

        barBg = bar.transform.GetChild(2).GetComponent<Image>();
        barBgRight = bar.transform.GetChild(3).GetComponent<Image>();
        currentBar = bar.transform.GetChild(4).GetComponent<RectMask2D>();

        Vector2 origin = barBg.rectTransform.anchoredPosition;

        barBg.rectTransform.sizeDelta = new Vector2(max, barBg.rectTransform.rect.height);
        currentBar.rectTransform.sizeDelta = new Vector2(current, currentBar.rectTransform.rect.height);

        Vector3 pos = barBgRight.rectTransform.anchoredPosition;
        pos.x = origin.x + max;
        barBgRight.rectTransform.anchoredPosition = pos;
    }

    private void Update()
    {
        if (playerHealth.currentHealth != previousHealth)
            DisplayHealthBar();

        if (shieldsHealth.currentHealth != previousShields)
            DisplayShieldsBar();

        if (player.GetComponent<PlayerController>().numMissiles != previousMissiles)
            DisplayMissilesBar();

        //int numBar = (int)Mathf.Ceil(gameState.playerHealth.currentHealth / 10);
        //Debug.Log(gameState.playerHealth.currentHealth + " / " + numBar);

        //gameState.playerHealth.currentHealth -= 1;

        //if (uiScoreText != null && gameState.score != 0)
        //    uiScoreText.text = "SCORE: " + gameState.score.ToString();
        //if (uiTimerText != null && gameState.timer != 0)
        //    uiTimerText.text = "TIME: " + Mathf.Floor(gameState.timer).ToString();
        //if (uiHealthText != null && gameState.playerHealth != null)
        //    uiHealthText.text = "HULL: " + gameState.playerHealth.currentHealth.ToString();
    }
}