using NUnit.Framework;
using UnityEngine;

public class ToolObjectTests
{
    private GameObject toolGO;
    private ToolObject tool;

    [SetUp]
    public void Setup()
    {
        // Tool GameObject and script
        toolGO = new GameObject("Tool");
        tool = toolGO.AddComponent<ToolObject>();

        // Set toolInfo using reflection (private field)
        var itemInfo = ScriptableObject.CreateInstance<ItemInfo>();
        itemInfo.itemName = "Test Tool";
        typeof(ToolObject)
            .GetField("toolInfo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(tool, itemInfo);

        // Set dummy PlayerManager singleton
        var playerGO = new GameObject("PlayerManager");
        var dummyPlayerManager = playerGO.AddComponent<PlayerManager>();
        dummyPlayerManager.inventoryManager = playerGO.AddComponent<DummyInventoryManager>(); // assign real component
        PlayerManager.instance = dummyPlayerManager;

        // Set dummy AudioManager singleton
        var audioGO = new GameObject("AudioManager");
        var dummyAudioManager = audioGO.AddComponent<DummyAudioManager>();
        AudioManager.instance = dummyAudioManager;

        // Set dummy TaskManager singleton
        var taskGO = new GameObject("TaskManager");
        taskGO.AddComponent<DummyTaskManager>();
        TaskManager.instance = taskGO.GetComponent<DummyTaskManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(toolGO);
        Object.DestroyImmediate(PlayerManager.instance.gameObject);
        Object.DestroyImmediate(AudioManager.instance.gameObject);
        Object.DestroyImmediate(TaskManager.instance.gameObject);
    }

    [Test]
    public void Interact_Pickable_PicksUpItemAndDisablesSelf()
    {
        tool.isPickable = true;

        tool.Interact();

        Assert.IsFalse(tool.gameObject.activeSelf);
    }

    [Test]
    public void Interact_NotPickable_DoesNothing()
    {
        tool.isPickable = false;

        tool.Interact();

        Assert.IsTrue(tool.gameObject.activeSelf);
    }
}

// Dummy implementations
public class DummyInventoryManager : InventoryManager
{
    public bool pickupCalled = false;
    public bool pickupResult = true;

    public override bool PickupItem(Item item)
    {
        pickupCalled = true;
        return pickupResult;
    }
}

public class DummyAudioManager : AudioManager
{
    public bool played = false;

    public override void PlaySound(AudioClip clip)
    {
        played = true;
    }
}

public class DummyTaskManager : TaskManager
{
    public override void IncrementTask(TaskTypes type)
    {
        Debug.Log("IncrementTask called with: " + type);
    }
}