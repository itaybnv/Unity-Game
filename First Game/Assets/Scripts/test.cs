using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
	[Tooltip("The target transform that this should follow")]
	[SerializeField] Transform target;

	[Space]

	[Tooltip("The x offset from the target")]
	[SerializeField] float xOffset;

	[Tooltip("The y offset from the target")]
	[SerializeField] float yOffset;

	[Space]
	[Tooltip("How close to the edge of the screen is the waypoint allowed to go? Must be a value between 0 and half of the screen width")]
	[SerializeField] float edgeThicknessX;
	[Tooltip("How close to the edge of the screen is the waypoint allowed to go? Must be a value between 0 and half of the screen height")]
	[SerializeField] float edgeThicknessY;

	[Space]
	[Tooltip("The transform of the image that should rotate to point the target")]
	[SerializeField] Transform arrow;

	[Tooltip("The rotational offset of the arrow")]
	[SerializeField] float arrowRotation;

	public Camera cam;

	void Start () {

		//finds the main camera
		cam = Camera.main;

		//error messages
		if (cam == null)
		{
			Debug.LogError("Couldn't find the main camera!");
		}

		if (edgeThicknessX < 0)
		{
			Debug.LogWarning("edgeThicknessX (" + edgeThicknessX + ") Has a value less than 0.");
		} else if (edgeThicknessX > Screen.width/2)
		{
			Debug.LogWarning("edgeThicknessX (" + edgeThicknessX + ") Has a value greater than Half the screen width (" + Screen.width/2 + ").");
		}

		if (edgeThicknessY < 0)
		{
			Debug.LogWarning("edgeThicknessY (" + edgeThicknessY + ") Has a value less than 0.");
		}
		else if (edgeThicknessY > Screen.height / 2)
		{
			Debug.LogWarning("edgeThicknessY (" + edgeThicknessY + ") Has a value greater than Half the screen height (" + Screen.height / 2 + ").");
		}
	}

	void LateUpdate () {

		//sets the desired position
		Vector3 targetPos = target.position + new Vector3(xOffset,yOffset,0);

		//makes sure it doesn't go out of the screen
		targetPos.x = Mathf.Clamp(targetPos.x, cam.ScreenToWorldPoint(Vector3.zero).x + edgeThicknessX,cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,cam.pixelHeight)).x - edgeThicknessX);
		targetPos.y = Mathf.Clamp(targetPos.y, cam.ScreenToWorldPoint(Vector3.zero).y + edgeThicknessY,cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,cam.pixelHeight)).y - edgeThicknessY);
		Debug.Log(targetPos);
		Debug.Log(cam.scaledPixelWidth);
		//actually sets the position
		transform.position = targetPos;

		//rotates the arrow
		if(arrow != null)
		{
			Vector3 rotation = target.position- transform.position;
			arrow.up = rotation;

			if(arrowRotation != 0)
			{
				arrow.Rotate(new Vector3(0, 0, arrowRotation));
			}

		}

	}
}
