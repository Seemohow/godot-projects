using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance { get; private set; }
	
	[Signal] public delegate void NewPlatformEventHandler(Vector3 platformPosition);
	[Signal] public delegate void GameOverEventHandler();
	
	public override void _Ready() {
		Instance = this;
	}
	
	public static void EmitNewPlatform(Vector3 platformPosition) {
		Instance.EmitSignal("NewPlatform", platformPosition);
	}
	
	public static void EmitGameOver() {
		Instance.EmitSignal("GameOver");
	}
}
