using System.Collections.Generic;
using UnityEngine;

public class PanelObject : Interactable
{
    [Header("Panel Attributes")]
    [SerializeField] private List<NailObject> _nails;

    public override void Interact()
    {
        // Cannot be interacted with if there are still nails
        if (CheckNails())
            return;

        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Checks if there is a crowbar in hand
        if (item != null && item.itemName.Equals("Crowbar"))
        {
            Destroy(gameObject);

            // Provide progress to related task
            TaskManager.instance.IncrementTask(TaskTypes.RemovePanel);
        }
    }

    public override string OnHover()
    {
        // Checks if there are still nails
        if (CheckNails())
            return "Need to remove all nails first";

        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Checks if there is a crowbar in hand
        if (item != null && item.itemName.Equals("Crowbar"))
        {
            return base.OnHover();
        }
        else
        {
            return $"Need crowbar to remove {objectName}!";
        }
    }

    // Returns if there are still nails attached to the panel or not
    private bool CheckNails()
    {
        foreach(NailObject nail in _nails)
        {
            if (nail != null)
                return true;
        }

        return false;
    }
}
