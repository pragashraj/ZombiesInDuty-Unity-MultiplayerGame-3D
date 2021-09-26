using UnityEngine;

public class cameraFollow : MonoBehaviour {

	public Transform player;
	public float smoothSpeed = 0.125f;
	public Vector3 offset;

    private void Start()
    {
		Vector3 desiredPosition = player.position + offset;
		transform.position = desiredPosition;
	}

    void FixedUpdate () {
		Vector3 desiredPosition = player.position + offset;
		Vector3 smmothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smmothedPosition;
	}
}
