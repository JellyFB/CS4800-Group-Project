using UnityEngine;

public class ToolObject : Interactable
{
    [SerializeField] private Tool tool;
    [SerializeField] private bool isPickable = true;

    private void Start()
    {
        // Makes sure the item name of the tool is the one showing in the feedback text
        objectName = tool.itemName;
    }

    // On interact behavior of objects
    public override void Interact()
    {
        if (!IsPickable())
            return;

        // Provide tool info to the inventory
        PlayerManager.instance.inventoryManager.PickupItem(Pick());

        // Hides GameObject
        gameObject.SetActive(false);
    }

    // On hover behavior not implemented
    public override void OnHover()
    {
        
    }

    public bool IsPickable()
    {
        return isPickable;
    }

    // Provide tool info to the inventory
    private Item Pick()
    {
        TaskManager.instance.UpdateTask(TaskTypes.GetTools);

        return tool;
    }
}
