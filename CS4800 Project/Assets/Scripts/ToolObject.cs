using UnityEngine;

public class ToolObject : MonoBehaviour, Interactable
{

    public void interact()
    {
        // TODO: Provide tool info to the inventory

        // Hides GameObject
        gameObject.SetActive(false);
    }
}
