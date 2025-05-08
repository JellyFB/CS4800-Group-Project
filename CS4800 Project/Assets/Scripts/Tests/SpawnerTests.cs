using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SpawnerTests
{
    private GameObject batterySpawnerObj;
    private BatterySpawner batterySpawner;

    private GameObject debrisSpawnerObj;
    private DebrisSpawner debrisSpawner;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // ---- Battery Spawner Setup ----
        batterySpawnerObj = new GameObject("BatterySpawner");
        batterySpawner = batterySpawnerObj.AddComponent<BatterySpawner>();

        var batteryPrefab = new GameObject("Battery");
        batteryPrefab.tag = "Battery";
        batteryPrefab.AddComponent<BatteryObject>();

        var batteryField = typeof(BatterySpawner).GetField("_batteryTypes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        batteryField.SetValue(batterySpawner, new GameObject[] { batteryPrefab });

        var batteryMinSpawnField = typeof(BatterySpawner).GetField("_minNumberOfSpawns", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        batteryMinSpawnField.SetValue(batterySpawner, 1);

        for (int i = 0; i < 3; i++)
        {
            var spawn = new GameObject("BatterySpawn" + i);
            spawn.tag = "Battery Spawn Point";
        }

        // ---- Debris Spawner Setup ----
        debrisSpawnerObj = new GameObject("DebrisSpawner");
        debrisSpawner = debrisSpawnerObj.AddComponent<DebrisSpawner>();

        var debrisPrefab = new GameObject("Debris");
        debrisPrefab.tag = "Debris";

        var debrisField = typeof(DebrisSpawner).GetField("_debrisTypes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        debrisField.SetValue(debrisSpawner, new GameObject[] { debrisPrefab });

        var debrisMinSpawnField = typeof(DebrisSpawner).GetField("_minNumberOfSpawns", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        debrisMinSpawnField.SetValue(debrisSpawner, 1);

        for (int i = 0; i < 3; i++)
        {
            var spawn = new GameObject("DebrisSpawn" + i);
            spawn.tag = "Debris Spawn Point";
        }

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        foreach (var obj in Object.FindObjectsByType<BatteryObject>(FindObjectsSortMode.None))
        {
            Object.Destroy(obj);
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator SpawnBattery_SpawnsBatteriesCorrectly()
    {
        // Act
        batterySpawner.SpawnBattery();

        // Wait a frame for instantiation
        yield return null;

        // Assert that at least one battery is spawned
        var spawnedBatteries = Object.FindObjectsByType<BatteryObject>(FindObjectsSortMode.None);
        Assert.IsTrue(spawnedBatteries.Length > 0, "No batteries were spawned.");

        // Check if attributes are set correctly
        foreach (var battery in spawnedBatteries)
        {
            Assert.AreEqual(false, battery.isPickable, "isPickable not set correctly.");
            Assert.AreEqual(true, battery.needCrowbar, "needCrowbar not set correctly.");
        }
    }

    [UnityTest]
    public IEnumerator SpawnDebris_SpawnsDebrisCorrectly()
    {
        debrisSpawner.SpawnDebris();
        yield return null;

        var spawnedDebris = GameObject.FindGameObjectsWithTag("Debris");
        Assert.IsTrue(spawnedDebris.Length > 0, "No debris objects were spawned.");
    }
}