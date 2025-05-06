using System.Collections;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;

public class PlayerTests
{
    GameObject player;
    CharacterController playerController;
    PlayerCam playerCam;
    Slider mockSlider;

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

        yield return null; // Let Unity finish component setup
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(player);
        yield return null;
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
}
