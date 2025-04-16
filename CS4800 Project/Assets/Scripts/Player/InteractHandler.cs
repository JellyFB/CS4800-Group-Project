using UnityEngine;

public class InteractHandler : MonoBehaviour
{
    [SerializeField] private float _playerRange = 5f;
    [SerializeField] private InventoryManager _playerInventory;
    private RaycastHit _hit;

    // Update is called once per frame
    void Update()
    {
        // DEBUG: Draws a ray in the editor
        Debug.DrawRay(transform.position, transform.forward * _playerRange, Color.green);

        // Checks if player presses E and ray hits an interactable in front of them.
        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(transform.position, transform.forward, out _hit, _playerRange, LayerMask.GetMask("Interactable")))
        {
            Interact();
        }
    }

    // Interacts with the object the player is looking at.
    private void Interact()
    {
        Interactable interactableObject = _hit.transform.gameObject.GetComponent<Interactable>();

        if (interactableObject.IsPickable())
            _playerInventory.PickupItem(interactableObject.Pick());

        interactableObject.Interact();
    }
}
