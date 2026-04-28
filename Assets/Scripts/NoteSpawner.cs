using UnityEngine;
using System.Collections.Generic;

public class NoteSpawner : MonoBehaviour
{
    public GameObject[] notePrefabs;   // 10 not
    public Transform[] spawnPoints;    // lokacije

    void Start()
    {
        SpawnNotes();
    }

    void SpawnNotes()
    {
        List<Transform> availableSpawns = new List<Transform>(spawnPoints);

        foreach (GameObject note in notePrefabs)
        {
            if (availableSpawns.Count == 0)
            {
                Debug.LogWarning("Ni dovolj spawn točk!");
                return;
            }

            int randomIndex = Random.Range(0, availableSpawns.Count);
            Transform spawnPoint = availableSpawns[randomIndex];

            Instantiate(note, spawnPoint.position, spawnPoint.rotation);

            availableSpawns.RemoveAt(randomIndex);
        }
    }
}