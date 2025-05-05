using UnityEngine;

public class NailObject : Interactable
{
    [SerializeField] private AudioClip drillAudio;
    [SerializeField] private AudioClip screwAudio;

     // Identifier for the specific object in game 
    private void Start()
    {
        objectName = "Nail";
    }

    public override void Interact()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Only interact if the item is a shovel
        if (item != null && (item.itemName.Equals("Drill") || item.itemName.Equals("Screwdriver")) )
        {
            TaskManager.instance.IncrementTask(TaskTypes.RemoveNails);

            // Play pickup audio using AudioManager
            if (item.itemName.Equals("Drill")) {
            AudioManager.instance.PlaySound(drillAudio);
            }

            if (item.itemName.Equals("Screwdriver")) {
            AudioManager.instance.PlaySound(screwAudio);
            }

            Destroy(gameObject);
        }
    }

    public override string OnHover()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Change text if player is holding a shovel
        if (item != null && (item.itemName.Equals("Drill") || item.itemName.Equals("Screwdriver")))
            return base.OnHover();
        else
            return $"Need a drill or screwdriver to remove {objectName}!";
    }
}

