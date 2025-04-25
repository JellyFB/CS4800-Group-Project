using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;

    public Vector2 MoveInput { get; private set; }

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
	MoveInput = _moveAction.ReadValue<Vector3>();
	JumpJustPressed = _jumpAction.WasPressedThisFrame();
	InteractInput = _interactAction.WasPressedThisFrame();
	MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
    }
}
