using UnityEngine;

public class Drop : MonoBehaviour
{
    public DropTypes dropType;

    public enum DropTypes
    {
        Health,
        Shields,
        Missiles,
        Component,
        Money
    }

    private GameObject player;
    GameState gameState;
    private Health playerHealth, shieldsHealth;

    private void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();
        player = GameObject.FindGameObjectWithTag("Player");

        playerHealth = player.GetComponent<Health>();
        shieldsHealth = player.transform.GetChild(4).GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (dropType)
        {
            case DropTypes.Health:
                playerHealth.RecoverHealth(20);
                break;

            case DropTypes.Shields:
                shieldsHealth.RecoverHealth(50);
                break;

            case DropTypes.Missiles:
                player.GetComponent<PlayerController>().numMissiles += 2;
                if (player.GetComponent<PlayerController>().numMissiles > player.GetComponent<PlayerController>().maxMissiles)
                    player.GetComponent<PlayerController>().numMissiles = player.GetComponent<PlayerController>().maxMissiles;
                break;

            case DropTypes.Money:
                gameState.money += 10;
                break;
        }

        player.GetComponent<PlayerController>().PlaySound();
    }
}