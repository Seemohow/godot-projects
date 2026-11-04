using Godot;
using System;

public partial class PlayerCam : Camera3D
{
	[Export] private Vector3 buffer = new Vector3(0, 12.0f, -14.0f);	
	[Export] private float smoothSpeed = 2.0f;	
	private Vector3 basePosition;
	
	public override void _Ready() {
		SignalHub.Instance.NewPlatform += _On_New_Platform;
		basePosition = Position;
	}

	public override void _PhysicsProcess(double delta) {
		if (Position.DistanceTo(basePosition) < 0.01f) {
			Position = basePosition;
		} else {
			Position = Position.Lerp(basePosition, (float)delta * smoothSpeed);
		}
	}
	
	public void _On_New_Platform(Vector3 position) {
		AdjustPosition(position);
	}

	private void AdjustPosition(Vector3 position) {
		basePosition = buffer + position;
	}
	
	public override void _ExitTree() {
		SignalHub.Instance.NewPlatform -= _On_New_Platform;
	}
}
