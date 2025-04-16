using System;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
	// Mouse sensitivity
	public float sensX;
	public float sensY;

	// Player
	public Transform player;

	// Rotation variables for storing
	float _xRotation;
	float _yRotation;

		// Start is called before first frame update
    	private void Start()
    	{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		// Update is called once per frame
    	private void Update()
    	{
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
}
