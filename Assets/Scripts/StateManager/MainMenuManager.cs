using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu, settingsMenu;

    private void Start()
    {
    }

    private void Update()
    {
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