using Godot;
using System;

public partial class Spawner : Node {
	[Export] public PackedScene platformScene;
	private Platform platform;
	
	public override void _Ready() {
		platform = GetNode<Platform>("Platform");
		platform.NewPlatform += _On_New_Platform;
	}

	private void SpawnPlatform(Vector3 oldPlatformPosition) {
		Platform newPlatform = platformScene.Instantiate<Platform>();
		newPlatform.Position = oldPlatformPosition + new Vector3(2, 5, -2);
		newPlatform.NewPlatform += _On_New_Platform;
		AddChild(newPlatform);
	}

	public void _On_New_Platform(Vector3 postion) {
		SpawnPlatform(postion);
	}
}
