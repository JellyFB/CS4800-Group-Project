using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BatterySpawner : MonoBehaviour
{
    // Battery Prefabs to Initialize
    [SerializeField] private GameObject[] _batteryTypes;

    // GameObject Spawnpoints where batteries will spawn
    private GameObject[] _spawnPoints;
    
    // Header for the in game engine to assign spawns
    [Header("Number of Spawns")]
    [SerializeField] private int _minNumberOfSpawns;
    private int _numberOfSpawns;
        
    // Identifying when the scene that is specified is actually loaded to "start" script
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Testing to see when the script should be run to spawn objects/assets
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name.Equals("Level2Scene") || scene.name.Equals("Level3Scene") || scene.name.Equals("Level4Scene")) {
            SpawnBattery();
        }
    }

    // Actual spawning of objects 
    public void SpawnBattery() {
        // Locates object spawns inside the scene
        _spawnPoints = GameObject.FindGameObjectsWithTag("Battery Spawn Point");

        // Randomizes how many objects to spawn
        _numberOfSpawns = Random.Range(_minNumberOfSpawns, _spawnPoints.Length);

        // Taking the lower value between the max amount and randomized amount 
        int objectsToSpawn = Mathf.Min(_numberOfSpawns, _spawnPoints.Length);

        // Keeping track of all available positions to spawn an object in that position
        List<int> availablePositions = new List<int>();

        // Adding all spawnable posiitions
        for (int i = 0; i < _spawnPoints.Length; i++) {
            availablePositions.Add(i);
        }

        // Loop to spawn objects into specified positions as well as a random object type from the given pool of assets/objects
        for (int i = 0; i < objectsToSpawn; i++) {
            if (availablePositions.Count == 0) {
                break;
            }

            int randomPos = Random.Range(0, availablePositions.Count);
            int spawnPointPos = availablePositions[randomPos];
            availablePositions.RemoveAt(randomPos);

            GameObject batterySpawn = _batteryTypes[Random.Range(0, _batteryTypes.Length)];

            Instantiate(batterySpawn, _spawnPoints[spawnPointPos].transform.position, _spawnPoints[spawnPointPos].transform.rotation);
         }

    }

}
