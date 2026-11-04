using Godot;
using System;

public partial class GameUi : Control {
	private ColorRect gameOverRect;

	public override void _UnhandledInput(InputEvent @event) {
		if (@event.IsActionPressed("reload")) {
			GetTree().ReloadCurrentScene();
		}
	}

	public override void _EnterTree() {
		GetTree().Paused = false;
	}

	public override void _Ready() {
		SignalHub.Instance.GameOver += _On_Game_Over;
		gameOverRect = GetNode<ColorRect>("GameOverRect");
		gameOverRect.Visible = false;
	}

	public void _On_Game_Over() {
		gameOverRect.Visible = true;
		GetTree().Paused = true;
	}
	
	public override void _ExitTree() {
		SignalHub.Instance.GameOver -= _On_Game_Over;
	}
}
