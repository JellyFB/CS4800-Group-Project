using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCam : MonoBehaviour
{
	public float sensX;
	public float sensY;

	public Transform orientation;

	float xRotation;
	float yRotation;

	// Start is called before first frame update
    	//private void Start()
    	//
        //	if (SceneManager.GetActiveScene().name == "SampleScene") {
        //    	Cursor.lockState = CursorLockMode.Locked;
        //    	Cursor.visible = false;
        //	}
    	//}

	// Update is called once per frame
    	private void Update()
    	{		
		// get mouse input
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

		yRotation += mouseX;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		// rotate cam and orientation
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    	}
}