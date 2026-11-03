using Godot;
using System;

public partial class PlayerCam : Camera3D
{
	[Export] private Vector3 buffer = new Vector3(0, 12.0f, -14.0f);	
	
	public override void _Ready()
	{
		SignalHub.Instance.NewPlatform += _On_New_Platform;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _On_New_Platform(Vector3 position) {
		AdjustPosition(position);
	}

	private void AdjustPosition(Vector3 position) {
		Position = buffer + position;
	}
}
