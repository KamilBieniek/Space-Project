using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour {

	public float earthDays;
	public float daysToGoRoundSun;
	public float speed;
	public Vector3 ellipseCenter;
	public Canvas canvas;
	public float angle;
	public float ellipseWidth;
	public float ellipseHeight;

	private CameraControls globalCamera;
	private TrailRenderer tr;
	private bool isSun;

	void Start () 
	{
		globalCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControls> ();

		if (this.name == "Sun Holder") 
		{
			isSun = true;
		} 
		else 
		{
			tr = GetComponent<TrailRenderer> ();
		}
	}

	void Update () {

		speed = globalCamera.globalSpeed;

		SelfRotation ();

		if (!isSun) 
		{
			PlanetNamesMovement ();
			AroundSunRotation ();
			TrailControl ();
		}
	}
		
	void TrailControl()
	{
		tr.time = (0.95f * daysToGoRoundSun) / speed;

		//Turning off the trail with the rest of UI.
		if (Input.GetKey (KeyCode.U)) {
			tr.emitting = false;
			tr.time = 0f;
		} 
		else 
		{
			tr.emitting = true;
		}
	}

	void PlanetNamesMovement()
	{
		//Planets name's follow their planets.
		canvas.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

		//Making names over planets always look at the camera direction, unless it's too close.
		if (Vector3.Distance (globalCamera.transform.position, canvas.transform.position) > 50) 
		{
			canvas.transform.LookAt (new Vector3(globalCamera.transform.position.x, canvas.transform.position.y, globalCamera.transform.position.z), worldUp: Vector3.up);
		}
	}

	void SelfRotation()
	{
			transform.Rotate(Vector3.up, -4 * Time.deltaTime * speed * 60 / earthDays);
	}

	void AroundSunRotation()
	{
		transform.position = new Vector3 (ellipseCenter.x + ellipseWidth * Mathf.Cos (angle*(Mathf.PI/180)), transform.position.y, ellipseCenter.z + ellipseHeight * Mathf.Sin (angle*(Mathf.PI/180)));
		angle = angle + 360 * Time.deltaTime * speed / daysToGoRoundSun;

		//Using angles periodicity to prevent it's value from going to infnity.
		if (angle > 360) 
		{
			angle = angle - 360;
		}
	}
}
