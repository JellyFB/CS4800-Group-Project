using System.Collections;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerTests
{
    GameObject player;
    CharacterController playerController;
    PlayerCam playerCam;

    // InteractHandler test-specific variables
    InteractHandler interactHandler;
    TextMeshProUGUI feedbackText;
    GameObject cameraObject;
    GameObject interactableObject;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Create the player GameObject
        player = new GameObject("Player");

        // Add PlayerInput and assign a mock InputActionAsset
        var playerInput = player.AddComponent<PlayerInput>();
    
        // Load the input actions asset from Resources (put your .inputactions file in Resources folder)
        var inputActions = Resources.Load<InputActionAsset>("Controls");
        playerInput.actions = inputActions;

        // Add UserInput script AFTER assigning actions
        player.AddComponent<UserInput>();

        // Add and configure PlayerController
        playerController = player.AddComponent<CharacterController>();
        player.AddComponent<Rigidbody>();
        player.AddComponent<Animator>();
        playerController.footstepsWalk = player.AddComponent<AudioSource>();
        playerController.footstepsSprint = player.AddComponent<AudioSource>();
        player.AddComponent<AudioListener>();

        // Add and configure PlayerCam
        playerCam = player.AddComponent<PlayerCam>();
        playerCam.MouseSensitivitySilder = player.AddComponent<Slider>();
        playerCam.player = player.transform;

        // Create camera object with InteractHandler
        cameraObject = new GameObject("Camera");
        cameraObject.transform.position = Vector3.zero;
        cameraObject.transform.forward = Vector3.forward;
        cameraObject.tag = "MainCamera"; // Needed for raycasting to work correctly
        cameraObject.AddComponent<Camera>();

        interactHandler = cameraObject.AddComponent<InteractHandler>();

        // Add a dummy TextMeshProUGUI for feedback
        GameObject textObject = new GameObject("FeedbackText");
        feedbackText = textObject.AddComponent<TextMeshProUGUI>();

        // Assign it to the private field via reflection
        SetPrivateField(interactHandler, "_interactFeedbackText", feedbackText);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(player);
        Object.Destroy(cameraObject);
        Object.Destroy(interactableObject);
        yield return null;
    }

    // Helper method to set private fields
    void SetPrivateField<T>(object obj, string fieldName, T value)
    {
        var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field?.SetValue(obj, value);
    }

    [UnityTest]
    public IEnumerator PlayerController_Resets_Position_When_Falling()
    {
        // Simulate player falling
        player.transform.position = new Vector3(0, -20f, 0);

        yield return new WaitForFixedUpdate();

        Assert.Greater(player.transform.position.y, -10f, "Player should reset to start position.");
    }

    [UnityTest]
    public IEnumerator PlayerCam_AdjustSpeed_Changes_Sensitivity()
    {
        // Manually call AdjustSpeed()
        playerCam.AdjustSpeed(50f);

        yield return null;

        Assert.AreEqual(180f, playerCam.sensX, 0.1f);
        Assert.AreEqual(180f, playerCam.sensY, 0.1f);
    }

    [UnityTest]
    public IEnumerator PlayerCam_Slider_Update_Changes_Sensitivity()
    {
        // Simulate slider value changed
        playerCam.MouseSensitivitySilder.value = 60f;
        playerCam.MouseSensitivitySilder.onValueChanged.Invoke(60f);

        yield return null;

        Assert.AreEqual(216f, playerCam.sensX, 0.1f);
        Assert.AreEqual(216f, playerCam.sensY, 0.1f);
    }

    // Added AdjustSpeed method here for completeness
    [Test]
    public void AdjustSpeed_ChangesSensitivityCorrectly()
    {
        // Arrange
        float expectedSensitivity = 180f;
        float newSpeed = 50f;

        // Act
        playerCam.AdjustSpeed(newSpeed);

        // Assert
        Assert.AreEqual(expectedSensitivity, playerCam.sensX, 0.1f);
        Assert.AreEqual(expectedSensitivity, playerCam.sensY, 0.1f);
    }

    // InteractHandler Test: Check if feedback text appears when looking at an interactable object
    [UnityTest]
    public IEnumerator InteractHandler_Shows_Interact_Text_When_Looking_At_Interactable()
    {
        // Set up interactable GameObject
        interactableObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        interactableObject.transform.position = cameraObject.transform.position + cameraObject.transform.forward * 1f;
        interactableObject.layer = LayerMask.NameToLayer("Interactable");

        // Add dummy Outline and TestInteractable
        interactableObject.AddComponent<Outline>();
        interactableObject.AddComponent<TestInteractable>();

        yield return null;

        // Simulate one frame so Update() runs
        interactHandler.Invoke("Update", 0f);

        yield return null;

        Assert.IsTrue(feedbackText.text.Contains("Press [E] to Interact"), "Feedback text should show interact message.");
    }

    // Test subclass for Interactable to use for testing
    public class TestInteractable : Interactable
    {
        private void Start()
        {
            objectName = "TestObject";
        }

        public override void Interact()
        {
            // Log interaction for testing
            Debug.Log($"{objectName} interacted with!");
        }

        public override string OnHover()
        {
            return base.OnHover(); // Uses default text + objectName
        }

        public override void OnHoverExit()
        {
            base.OnHoverExit();
        }
    }
}