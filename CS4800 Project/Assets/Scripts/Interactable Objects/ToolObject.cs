using UnityEngine;

public class ToolObject : MonoBehaviour, Interactable
{
    [SerializeField] private Tool tool;
    [SerializeField] private bool isPickable = true;
    public void Interact()
    {
        // TODO: Provide tool info to the inventory

        // Hides GameObject
        gameObject.SetActive(false);
    }

    public bool IsPickable()
    {
        return isPickable;
    }

    public Item Pick()
    {
        return tool;
    }
}
