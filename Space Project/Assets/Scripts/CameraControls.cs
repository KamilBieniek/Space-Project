using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControls : MonoBehaviour {

	public float globalSpeed;
	public Text information;
	public GameObject canvas;

	private Quaternion initialRotation;
	private Vector3 initialPosition;


	void Start () 
	{
		globalSpeed = 1f;
		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}
	

	void Update () 
	{
		//Changing UI text.
		if (globalSpeed == 1f) 
		{
			information.text = "1 second = " + globalSpeed + " day";
		} 
		else 
		{
			information.text = "1 second = " + globalSpeed + " days";
		}

		CameraMovement ();
		CameraRestart ();
		HidingUI ();
	}

	void HidingUI()
	{
		if (Input.GetKeyDown (KeyCode.U))
		{
			canvas.SetActive(false);
		}
		if (Input.GetKeyUp (KeyCode.U))
		{
			canvas.SetActive(true);
		}
	}

	void CameraRestart()
	{
		if (Input.GetKey (KeyCode.R)) 
		{
			transform.position = initialPosition;
			transform.rotation = initialRotation;
		}
	}

	void CameraMovement()
	{
		//Moving the camera.
		if (Input.GetKey (KeyCode.W))
		{
			transform.position = transform.position + transform.forward * 45 * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S))
		{
			transform.position = transform.position - transform.forward * 45 * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A))
		{
			transform.position = transform.position - transform.right * 45 * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D))
		{
			transform.position = transform.position + transform.right * 45 * Time.deltaTime;
		}

		//Rotating the camera.
		if (Input.GetKey (KeyCode.Keypad6))
		{
			transform.Rotate (Vector3.up, relativeTo:Space.World);
		}
		if (Input.GetKey (KeyCode.Keypad4))
		{
			transform.Rotate (Vector3.down, relativeTo:Space.World);
		}
		if (Input.GetKey (KeyCode.Keypad8))
		{
			transform.Rotate (Vector3.left, relativeTo:Space.Self);
		}
		if (Input.GetKey (KeyCode.Keypad2))
		{
			transform.Rotate (Vector3.right, relativeTo:Space.Self);
		}
	}

	//Just to have access from UI's slider.
	public void speedChange (float setSpeed)
	{
		globalSpeed = setSpeed;
	}
		
}
