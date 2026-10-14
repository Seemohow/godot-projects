using Godot;
using System;

public partial class Player : CharacterBody3D {
	private readonly float gravity = 35.0f;

	private const float jumpForce = 23.0f;

	private Label label;
	
	public override void _Ready()
	{
		label = GetNode<Label>("Label");
	}

	public override void _PhysicsProcess(double delta) {
		HandleGravity(delta);
		MoveAndSlide();
	}

	private void HandleGravity(double delta) {
		Vector3 velocity = Velocity;
		velocity.Y -= gravity * (float)delta;
		Velocity = velocity;
		if (IsOnFloor()) {
			velocity.Y += jumpForce;
			Velocity = velocity;
		}

		label.Text = Math.Floor(GetPosition().Y * 100).ToString();
	}
	
	
}
