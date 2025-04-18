using TMPro;
using UnityEngine;

// Interact Handler must stay on camera to inherit camera rotation
public class InteractHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryManager _playerInventory;
    [SerializeField] private TextMeshProUGUI _interactFeedbackText;

    [Header("Attributes")]
    [SerializeField] private float _playerRange = 5f;
    
    private RaycastHit _hit;

    // Update is called once per frame
    void Update()
    {
        // DEBUG: Draws a ray in the editor
        Debug.DrawRay(transform.position, transform.forward * _playerRange, Color.green);

        // Check if the player is currently looking at an interactable
        if (Physics.Raycast(transform.position, transform.forward, out _hit, _playerRange, LayerMask.GetMask("Interactable")))
        {
            // Get the interactable script
            Interactable interactableObject = _hit.transform.gameObject.GetComponent<Interactable>();

            // OnHover behavior of the interactable object
            LookAt(interactableObject);

            // If player is pressing E, interact with the object
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact(interactableObject);
            }
        }
        else
        {
            // Clears the feedback text
            _interactFeedbackText.text = "";
        }
    }

    // OnHover behavior of the interactable object
    // Also updates the feedback text to show name of interactable object
    private void LookAt(Interactable interactableObject)
    {
        string text = interactableObject.OnHover();
        _interactFeedbackText.text = text;
    }


    // Interacts with the object the player is looking at.
    private void Interact(Interactable interactableObject)
    {
        interactableObject.Interact();
    }
}
