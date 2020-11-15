using Assets.Scripts.StateManager;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu, settingsMenu, gameState;

    private void Awake()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void OnClickSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnClickBack()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}