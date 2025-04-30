using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;

    public UnityEngine.Vector3 MoveInput { get; private set; }

    public bool JumpJustPressed { get; private set; }

    public bool InteractInput { get; private set; }

    public bool MenuOpenCloseInput { get; private set;}


    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _interactAction;
    private InputAction _menuOpenCloseAction;

    private void Awake()
    {
	if (instance == null) {
		instance = this;
	} else {
        Destroy(gameObject);
        return;
    }

	_playerInput = GetComponent<PlayerInput>();
	SetupInputActions();
    }
    
    private void Update()
    {
	UpdateInputs();
    }

    private void SetupInputActions()
    {
	_moveAction = _playerInput.actions["Move"];
	_jumpAction = _playerInput.actions["Jump"];
	_interactAction = _playerInput.actions["Interact"];
	_menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
    }

    private void UpdateInputs()
    {
	UnityEngine.Vector2 moveInput2D = _moveAction.ReadValue<UnityEngine.Vector2>();
    MoveInput = new UnityEngine.Vector3(moveInput2D.x, 0f, moveInput2D.y); // Converts to Vector3 for movement
	JumpJustPressed = _jumpAction.WasPressedThisFrame();
	InteractInput = _interactAction.WasPressedThisFrame();
	MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
    }
}
