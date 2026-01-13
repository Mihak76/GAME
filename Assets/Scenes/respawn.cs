using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    private GameObject currentPlayer;

    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}