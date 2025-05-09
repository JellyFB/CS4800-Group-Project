using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Vector3 _startPos;
    Rigidbody _rb;
    Ray _groundCheck;
    public float moveSpeed;
    public float jumpForce;


    private Animator animator;

    public AudioSource footstepsWalk, footstepsSprint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _startPos = transform.position;
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
        
        // Check for sprint toggle
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed = 4f;
        } else {
            moveSpeed = 2f;
        }

        // Player movement
        Vector3 input = UserInput.instance.MoveInput;
        Vector3 moveVector = input * moveSpeed * Time.fixedDeltaTime;
        transform.Translate(moveVector, Space.Self);

        // Animaton
        if (input.magnitude < 0.1f) {
            // Idle
            animator.SetFloat("Speed", 0);
        } else if (Input.GetKey(KeyCode.LeftShift)) {
            // Run
            animator.SetFloat("Speed", 1);
        } else {
            // Walk
            animator.SetFloat("Speed", 0.5f);
        }

        // Player jump
        if(UserInput.instance.JumpJustPressed && IsGrounded())
        {
            var linVel = _rb.linearVelocity;
            linVel.y = 0f;
            _rb.linearVelocity = linVel;
            _rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

        // Audio
        if (input.magnitude > 0.1f) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                footstepsSprint.enabled = true;
                footstepsWalk.enabled = false;
            } else {
                footstepsWalk.enabled = true;
                footstepsSprint.enabled = false;
            }
        } else {
            footstepsWalk.enabled = false;
            footstepsSprint.enabled = false;
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

    // Disables footsteps when player finishes
    public void DisableFootsteps()
    {
        if (footstepsWalk != null) footstepsWalk.enabled = false;
        if (footstepsSprint != null) footstepsSprint.enabled = false;
    }
}

// Ground drag doesn't work properly