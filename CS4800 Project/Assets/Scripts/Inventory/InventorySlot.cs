using UnityEngine;

// A gameobject that handles an inventory slot of an inventory.
public class InventorySlot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GetComponentInParent<InventoryManager>() == null)
            Debug.LogError($"{this} is not under a parent InventoryManager!");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
