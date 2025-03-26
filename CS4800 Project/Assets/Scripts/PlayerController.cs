using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Vector3 _startPos;
    Rigidbody _rb;
    Ray _groundCheck;
    public float moveSpeed;
    public float jumpForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _startPos = transform.position;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Sets the ground-check ray at the player's position.
        Vector3 rayPos = transform.position;
        rayPos.y += 1f;
        _groundCheck = new Ray(transform.position, Vector3.down);

        // DEBUG: Draws a ray in the editor
        Debug.DrawRay(rayPos, Vector3.down * 2f, Color.green);

        // Resets player position if player falls a certain distance.
        if (transform.position.y < -10f)
        {
            transform.position = _startPos;
        }

        // TODO: Animations 

        // Player movement
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))
            * Time.fixedDeltaTime * moveSpeed, Space.Self);

        // Player jump
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            var linVel = _rb.linearVelocity;
            linVel.y = 0f;
            _rb.linearVelocity = linVel;
            _rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

        // TODO: Player mouse rotation
    }

    private void LateUpdate()
    {
        IsGrounded();
    }

    // Checks if the player is grounded using ray-casting
    bool IsGrounded()
    {
        return Physics.Raycast(_groundCheck, 1f);
    }
}

// Ground drag doesn't work properly