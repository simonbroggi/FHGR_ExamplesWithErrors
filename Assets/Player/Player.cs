using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Mathematics.math;


public class Player : MonoBehaviour
{
	public InputAction playerInput;

	public float movementSpeed = 4;
	public float rotationSpeed = 180f;
	public float mouseSensitivity = 5f;

	public float startingVerticalEyeAngle = 10f;

	private CharacterController characterController;

	void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}
	void OnEnable()
	{
		playerInput.Enable();
	}
	void OnDisable()
	{
		playerInput.Disable();
	}

	void Update()
	{
		Move();
	}

	public Vector3 Move()
	{
		UpdatePosition();
		return transform.localPosition;
	}

	void UpdatePosition()
	{
		// Old input system code:
		// var movement = new Vector2(
		// 	Input.GetAxis("Horizontal"),
		// 	Input.GetAxis("Vertical"));

		var movement = playerInput.ReadValue<Vector2>();

		// Normalize diagonal movement
		float sqrMagnitude = movement.sqrMagnitude;
		if (sqrMagnitude > 1f)
		{
			movement /= Mathf.Sqrt(sqrMagnitude);
		}

		// Apply movement speed
		movement *= movementSpeed;

		// var forward = new Vector2(
		// 	Mathf.Sin(eyeAngles.x * Mathf.Deg2Rad),
		// 	Mathf.Cos(eyeAngles.x * Mathf.Deg2Rad));
		// var right = new Vector2(forward.y, -forward.x);

		// camera relative movement
		Vector3 forward3D = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
		Vector2 forward = new(forward3D.x, forward3D.z);
		Vector2 right = new(forward.y, -forward.x);
		
		movement = right * movement.x + forward * movement.y;
		characterController.SimpleMove(new Vector3(movement.x, 0f, movement.y));
	}
}
