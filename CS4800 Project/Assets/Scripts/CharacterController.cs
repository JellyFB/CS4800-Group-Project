using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Vector3 startPos;
    Rigidbody rb;
    Ray groundCheck;
    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Sets the ground-check ray at the player's position.
        Vector3 rayPos = transform.position;
        rayPos.y += 1f;
        groundCheck = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(rayPos, Vector3.down * 2f, Color.green);

        // Resets player position if player falls a certain distance.
        if (transform.position.y < 90f)
        {
            transform.position = startPos;
        }

        // TODO: Animations 

        // Player movement
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))
            * Time.fixedDeltaTime * moveSpeed, Space.Self);

        // Player jump
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            var linVel = rb.linearVelocity;
            linVel.y = 0f;
            rb.linearVelocity = linVel;
            rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
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
        if (Physics.Raycast(groundCheck, 1f))
        {
            isGrounded = true;
            return true;
        }
        else
        {
            isGrounded = false;
            return false;
        }
    }
}

// Ground drag doesn't work properly