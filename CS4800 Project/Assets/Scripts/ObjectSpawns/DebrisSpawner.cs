using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebrisSpawner : MonoBehaviour
{
    // Debris Prefabs to Initialize
    [SerializeField] private GameObject[] _debrisTypes;

    // GameObject Spawnpoints where debris can spawn on
    private GameObject[] _spawnPoints;

    [Header("Number of Spawns")]
    [SerializeField] private int _minNumberOfSpawns;
    private int _numberOfSpawns;
    
    void OnEnable() {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name.Equals("Level1Scene") || scene.name.Equals("Level2Scene")) {
            SpawnDebris();
        }
    }

    public void SpawnDebris() {
        _spawnPoints = GameObject.FindGameObjectsWithTag("Debris Spawn Point");
        _numberOfSpawns = Random.Range(_minNumberOfSpawns,_spawnPoints.Length);

        int objectsToSpawn = Mathf.Min(_numberOfSpawns, _spawnPoints.Length);

        List<int> availablePositions = new List<int>();

        for (int i = 0; i < _spawnPoints.Length; i++) {
            availablePositions.Add(i);
        }
            
        for (int i = 0; i < objectsToSpawn; i++) {
            if (availablePositions.Count == 0) {
                break;
            }

            int randomPos = Random.Range(0, availablePositions.Count);
            int spawnPointPos = availablePositions[randomPos];
            availablePositions.RemoveAt(randomPos);

            GameObject debrisSpawn = _debrisTypes[Random.Range(0, _debrisTypes.Length)];

            Instantiate(debrisSpawn, _spawnPoints[spawnPointPos].transform.position, _spawnPoints[spawnPointPos].transform.rotation);
        }
    }
}
