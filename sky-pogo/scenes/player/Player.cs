using Godot;
using System;

public partial class Player : CharacterBody3D {
	private readonly float gravity = 35.0f;
	private AnimationPlayer animationPlayer;
	private const float jumpForce = 23.0f;

	
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("character-male-d2/AnimationPlayer");
	}

	public override void _PhysicsProcess(double delta) {
		HandleGravity(delta);
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
