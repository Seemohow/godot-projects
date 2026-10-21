using Godot;
using System;

public partial class Player : CharacterBody3D {
	private readonly float gravity = 30.0f;
	private AnimationPlayer animationPlayer;
	private const float jumpForce = 23.0f;
	private const float rotationSpeed = 10.0f;
	private const float moveSpeed = 8.0f;
	private Vector2 inputDirection = Vector2.Zero;

	
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("character-male-d2/AnimationPlayer");
	}

	public override void _PhysicsProcess(double delta) {
		HandleGravity(delta);
		HandleMovement();
		HandleRotation(delta);
		MoveAndSlide();
		HandleAnimation();
	}

	private void HandleAnimation() {
		if (Velocity.Y > 0) {
			animationPlayer.Play("jump");
		}
		else {
			animationPlayer.Play("fall");
		}
	}

	private void HandleMovement() {
		inputDirection = Input.GetVector("ui_right", "ui_left", "ui_down", "ui_up");

		Velocity = new Vector3(
			inputDirection.X * moveSpeed,
			Velocity.Y,
			inputDirection.Y * moveSpeed
		);
	}

	private void HandleRotation(double delta) {
		if (inputDirection == Vector2.Zero) {
			return;
		}

		float targetAngle = Mathf.Atan2(inputDirection.X, inputDirection.Y);

		Rotation = new Vector3(
			Rotation.X,
			Mathf.LerpAngle(Rotation.Y, targetAngle, (float)delta * rotationSpeed),
			Rotation.Z
		);
	}
	
	private void HandleGravity(double delta) {
		Vector3 velocity = Velocity;
		velocity.Y -= gravity * (float)delta;
		Velocity = velocity;
		if (IsOnFloor()) {
			velocity.Y += jumpForce;
			Velocity = velocity;
		}
	}
	
	
}
