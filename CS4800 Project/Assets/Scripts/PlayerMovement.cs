using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement")]
	public float moveSpeed;
	public float groundDrag;

	[Header("Ground Check")]
	public float playerHeight;
	public LayerMask whatIsGround;
	bool _grounded;

	public Transform orientation;

	float _horizontalInput;
	float _verticalInput;

	Vector3 _moveDirection;

	Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
		_rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
		// ground check
		_grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
		MyInput();

		// handle drag
		if (_grounded) {
			_rb.linearDamping = groundDrag;
		} else {
			_rb.linearDamping = 0;
		}
		// drag not working
    }

    private void MyInput()
    {
		_horizontalInput = Input.GetAxisRaw("Horizontal");
		_verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
		// calculate movement direction
		_moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
		_rb.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
