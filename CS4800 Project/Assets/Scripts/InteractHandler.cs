using UnityEngine;

public class InteractHandler : MonoBehaviour
{
    [SerializeField] private float _playerRange = 5f;
    private RaycastHit _hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG: Draws a ray in the editor
        Debug.DrawRay(transform.position, transform.forward * _playerRange, Color.green);

        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(transform.position, transform.forward, out _hit, _playerRange, LayerMask.GetMask("Interactable")))
        {
            Interactable interactableObject = _hit.transform.gameObject.GetComponent<Interactable>();
            Debug.Log(interactableObject);

            interactableObject.interact();
        }
    }
}
