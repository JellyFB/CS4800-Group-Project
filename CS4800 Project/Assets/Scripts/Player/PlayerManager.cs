using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Singleton
    public static PlayerManager instance;

    [Header("References")]
    public InventoryManager inventoryManager;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
}
