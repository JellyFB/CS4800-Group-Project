using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebrisSpawner : MonoBehaviour
{
    public GameObject[] debrisTypes;
    public GameObject[] spawnPoints;

    public int numberOfSpawns = 7;
    
        
    void OnEnable() {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        
        if (scene.name == "InitialLevelTest") {
            SpawnDebris();
        }
    }

    public void SpawnDebris() {
            
            //fill spawnPositions
            spawnPoints = GameObject.FindGameObjectswithTag("debrisSpawnPoint");
            numberOfSpawns = Random.Range(0,spawnPoints.Length);

            int objectsToSpawn = Mathf.Min(numberOfSpawns, spawnPoints.Length);

            List<int> availablePositions = new List<int>();

            for (int i = 0; i < spawnPoints.Length; i++) {
                availablePositions.Add(i);
            }
            
            for (int i = 0; i < objectsToSpawn; i++) {
                if (availablePositions.Count == 0) {
                    break;
                }

                int randomPos = Random.Range(0, availablePositions.Count);
                int spawnPointPos = availablePositions[randomPos];
                availablePositions.RemoveAt(randomPos);

                GameObject debrisSpawn = debrisTypes[Random.Range(0, debrisTypes.Length)];

                Instantiate(debrisSpawn, spawnPoints[spawnPointPos].transform.position, spawnPoints[spawnPointPos].transform.rotation);

            }
    }
}
