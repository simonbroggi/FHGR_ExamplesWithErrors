// 9/1/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Mathematics.math;

public class Player : MonoBehaviour
{
	public InputAction playerInput;
	public InputAction jumpInput; // Input action for jumping

	public float movementSpeed = 4;
	public float rotationSpeed = 180f;
	public float mouseSensitivity = 5f;

	public float startingVerticalEyeAngle = 10f;

	public float jumpHeight = 2f; // Height of the jump
	public float gravity = -9.81f; // Gravity force

	private CharacterController characterController;
	private Transform rotationTransform;

	private float verticalVelocity = 0f; // Tracks vertical movement
	private bool isGrounded = false; // Tracks if the player is on the ground

	void Awake()
	{
		characterController = GetComponent<CharacterController>();
		rotationTransform = transform.GetChild(0);
	}

	void OnEnable()
	{
		playerInput.Enable();
		jumpInput.Enable(); // Enable jump input
	}

	void OnDisable()
	{
		playerInput.Disable();
		jumpInput.Disable(); // Disable jump input
	}

	void Update()
	{
		Move();
		HandleJumpAndGravity();
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
		var movement = playerInput.ReadValue<Vector2>();

		// Normalize diagonal movement
		float sqrMagnitude = movement.sqrMagnitude;
		if (sqrMagnitude > 1f)
		{
			movement /= Mathf.Sqrt(sqrMagnitude);
		}

		// Apply movement speed
		movement *= movementSpeed;

		// Camera-relative movement
		Vector3 forward3D = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
		Vector2 forward = new(forward3D.x, forward3D.z);
		Vector2 right = new(forward.y, -forward.x);

		movement = right * movement.x + forward * movement.y;
		Vector3 oldPosition = transform.position;

		// Include vertical velocity for jumping
		Vector3 moveVector = new Vector3(movement.x, verticalVelocity, movement.y);
		characterController.Move(moveVector * Time.deltaTime);

		Vector3 newPosition = transform.position;
		Vector3 delta = newPosition - oldPosition;
		return delta;
	}

	/// <summary>
	/// Handles jumping and gravity.
	/// </summary>
	void HandleJumpAndGravity()
	{
		isGrounded = characterController.isGrounded;

		if (isGrounded && verticalVelocity < 0)
		{
			verticalVelocity = -2f; // Small downward force to keep grounded
		}

		// Check for jump input
		if (isGrounded && jumpInput.triggered)
		{
			verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity
		}

		// Apply gravity
		verticalVelocity += gravity * Time.deltaTime;
	}
}
