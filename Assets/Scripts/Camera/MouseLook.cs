using UnityEngine;

public class MouseLook : MonoBehaviour {
	public float mouseSensitivity = 30f;
	public Transform player;
	public float xRotation;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () {
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
	}
}
