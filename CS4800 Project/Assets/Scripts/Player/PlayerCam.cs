using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
	// Mouse sensitivity
	public float sensX = 180f;
	public float sensY = 180f;


	// Player
	public Transform player;
	public Slider MouseSensitivitySilder;

	// Rotation variables for storing
	float _xRotation;
	float _yRotation;

    // Start is called before first frame update
    private void Start()
    	{
		MouseSensitivitySilder.onValueChanged.AddListener((v) => {AdjustSpeed(v);});
		sensX = PlayerPrefs.GetFloat("currentSensitivity", 180);
		sensY = PlayerPrefs.GetFloat("currentSensitivity", 180);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Update is called once per frame
    	private void Update()
    	{
		
		PlayerPrefs.SetFloat("currentSensitivity", sensX);
		PlayerPrefs.SetFloat("currentSensitivity", sensY);

		// get mouse input
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

		_yRotation += mouseX;
		_xRotation -= mouseY;

		_xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

		// rotate cam and orientation
		transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
		player.rotation = Quaternion.Euler(0, _yRotation, 0);
    	}

	public void AdjustSpeed(float newSpeed) {
		sensX = 360 * newSpeed/100;
		sensY = 360 * newSpeed/100;
	}
}
