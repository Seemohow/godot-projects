using Godot;
using System;

public partial class Spawner : Node {
	[Export] public PackedScene platformScene;
	
	private void SpawnPlatform(Vector3 oldPlatformPosition) {
		Platform newPlatform = platformScene.Instantiate<Platform>();
		newPlatform.Position = oldPlatformPosition + new Vector3(2, 5, -2);
		AddChild(newPlatform);
	}

	public void _On_New_Platform(Vector3 postion) {
		SpawnPlatform(postion);
	}
}
