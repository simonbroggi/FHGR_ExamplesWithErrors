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
	private Transform rotationTransform;

	void Awake()
	{
		characterController = GetComponent<CharacterController>();
		rotationTransform = transform.GetChild(0);
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

	public void Move()
	{
		Vector3 delta = UpdatePosition();

		// Rotate the player to face the direction of movement, if there is any.
		if (delta.sqrMagnitude > 0.0001f)
			rotationTransform.rotation = Quaternion.RotateTowards(rotationTransform.rotation, Quaternion.LookRotation(delta), rotationSpeed * Time.deltaTime);
	}


	/// <summary>
	/// Updates the player's position based on input.
	/// </summary>
	/// <returns>The change in position.</returns>
	Vector3 UpdatePosition()
	{
		// Old input system code:
		// var movement = new Vector2(
		// 	Input.GetAxis("Horizontal"),
		// 	Input.GetAxis("Vertical"));

		// New input system code:
		var movement = playerInput.ReadValue<Vector2>();

		// Normalize diagonal movement
		float sqrMagnitude = movement.sqrMagnitude;
		if (sqrMagnitude > 1f)
		{
			movement /= Mathf.Sqrt(sqrMagnitude);
		}

		// Apply movement speed
		movement *= movementSpeed;

		// camera relative movement
		Vector3 forward3D = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
		Vector2 forward = new(forward3D.x, forward3D.z);
		Vector2 right = new(forward.y, -forward.x);

		movement = right * movement.x + forward * movement.y;
		Vector3 oldPosition = transform.position;
		characterController.SimpleMove(new Vector3(movement.x, 0f, movement.y));
		Vector3 newPosition = transform.position;
		Vector3 delta = newPosition - oldPosition;
		return delta;
	}
}
