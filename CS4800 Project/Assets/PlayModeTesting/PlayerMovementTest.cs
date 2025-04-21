using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovementTest
{
    private GameObject _player;
    private CharacterController _controller;
    private Rigidbody _playerRb;
    private Keyboard _keyboard;
    private Vector3 _originalPosition;

    InputTestFixture inputTest = new InputTestFixture();

    [UnitySetUp]
    public IEnumerator Setup()
    {
        // Initialize the Input System
        InputSystem.AddDevice<Keyboard>();
        _keyboard = InputSystem.GetDevice<Keyboard>();

        // Load test scene
        yield return SceneManager.LoadSceneAsync("Level1Scene", LoadSceneMode.Single);

        // Find player
        _player = GameObject.FindGameObjectWithTag("Character");
        Assert.IsNotNull(_player, "Player not found in scene!");

        // Get components
        _player.transform.position = Vector3.up * 2f; // Start above floor
        _playerRb = _player.GetComponent<Rigidbody>();
        _controller = _player.GetComponent<CharacterController>();
        _originalPosition = _player.transform.position;

        // Ensure the player starts in a consistent state
        _player.transform.position = _originalPosition;
        _playerRb.linearVelocity = Vector3.zero;
            //Not checking animations yet --not finalized--
        // _controller.animator = _player.AddComponent<Animator>();

        yield return new WaitForFixedUpdate(); // Wait for physics setup
    }

    [UnityTearDown]
    public IEnumerator Teardown()
    {
        Object.Destroy(_player);
        InputSystem.RemoveDevice(_keyboard);
        yield return null;
    }

    // --- Movement Tests ---
    [UnityTest]
    public IEnumerator Player_Moves_Forward_On_W_Key()
    {
        // Wait to land on floor first
        yield return new WaitUntil(() => _controller.IsGrounded());
        float startY = _player.transform.position.y;

        // Simulate W key press
        inputTest.Press(_keyboard.wKey);
        yield return new WaitForSeconds(0.5f);

        Assert.Greater(
            _player.transform.position.z, 
            0.1f, 
            "Player did not move forward!"
        );
        inputTest.Release(_keyboard.wKey);
    }

    [UnityTest]
    public IEnumerator Player_Sprints_On_Shift_Key()
    {
        float walkDistance = 0f;
        float sprintDistance = 0f;

        // Test walk speed
        inputTest.Press(_keyboard.wKey);
        yield return new WaitForSeconds(1f);
        walkDistance = _player.transform.position.z;
        inputTest.Release(_keyboard.wKey);

        // Reset position
        _player.transform.position = Vector3.up * 2f;
        yield return new WaitForFixedUpdate();

        // Test sprint speed
        inputTest.Press(_keyboard.wKey);
        inputTest.Press(_keyboard.leftShiftKey);
        yield return new WaitForSeconds(1f);
        sprintDistance = _player.transform.position.z;
        inputTest.Release(_keyboard.wKey);
        inputTest.Release(_keyboard.leftShiftKey);
        Assert.Greater(
            sprintDistance, 
            walkDistance * 1.5f, 
            "Sprint speed not faster than walk!"
        );
    }

    // --- Jump Test ---
    [UnityTest]
    public IEnumerator Player_Jumps_On_Space_Key()
    {
        // Wait to land on floor first
        yield return new WaitUntil(() => _controller.IsGrounded());
        float startY = _player.transform.position.y;

        // Press Space
        inputTest.Press(_keyboard.spaceKey);
        yield return new WaitForFixedUpdate();
        inputTest.Release(_keyboard.spaceKey);

        yield return new WaitForSeconds(0.2f);
        Assert.Greater(
            _player.transform.position.y, 
            startY + 0.5f, 
            "Player did not jump!"
        );
    }
}
