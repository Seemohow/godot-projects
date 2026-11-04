using Godot;
using System;

using Godot.Collections;

public partial class Spawner : Node {
	[Export] public Array<PackedScene> platformScenes;
	private Platform platform;
	private readonly Vector2 offsetSide =  new Vector2(1.7f, 4);
	private readonly Vector2 offsetUp =  new Vector2(3.7f, 5.5f);
	private Random random = new Random();
	public override void _Ready() {
		platform = GetNode<Platform>("Platform");
		SignalHub.Instance.NewPlatform += _On_New_Platform;
	}

	private float getRandomOffset(Vector2 offsetRange) {
		float magnitude = (float)random.NextDouble() + offsetRange.Y - offsetRange.X;
		
		if (random.NextDouble() < 0.5) {
			return magnitude;
		}
		return -magnitude;
	}
	
	private void SpawnPlatform(Vector3 oldPlatformPosition) {
		Platform newPlatform = platformScenes.PickRandom().Instantiate<Platform>();
		newPlatform.Position = oldPlatformPosition + new Vector3(
			getRandomOffset(offsetSide),
			(float)random.NextDouble() + offsetUp.Y - offsetUp.X,
			getRandomOffset(offsetSide)
			);
		AddChild(newPlatform);
	}

	public void _On_New_Platform(Vector3 position) {
		SpawnPlatform(position);
	}

	public override void _ExitTree() {
		SignalHub.Instance.NewPlatform -= _On_New_Platform;
	}
}
