using Godot;
using System;

public partial class Platform : Node3D {
	
	private AnimationPlayer animationPlayer;
	private Timer timer;
	private bool isHit = false;
	[Signal] public delegate void NewPlatformEventHandler(Vector3 platformPosition);
	
	public override void _Ready() {
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		timer = GetNode<Timer>("Timer");
	}
	
	public void _On_Timer_Timeout() {
		animationPlayer.Play("vanish");
	}
	
	public void _On_Animation_Finished(string animName) {
		if (animName == "vanish") {
			QueueFree();
		}
	}

	public void _On_Area_3d_Body_Entered(Node3D body) {
		if (body is Player && !isHit) {
			isHit = true;
			timer.Start();
			EmitSignal(SignalName.NewPlatform, Position);
		}
	}
}
