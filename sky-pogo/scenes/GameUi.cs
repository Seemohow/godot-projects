using Godot;
using System;

public partial class GameUi : Control {
	private ColorRect gameOverRect;
	private Label scoreLabel;
	private Label highScoreLabel;

	public override void _UnhandledInput(InputEvent @event) {
		if (gameOverRect.Visible && @event.IsActionPressed("reload")) {
			GetTree().ReloadCurrentScene();
		}
	}

	public override void _EnterTree() {
		GetTree().Paused = false;
	}

	public override void _Ready() {
		SignalHub.Instance.GameOver += _On_Game_Over;
		SignalHub.Instance.NewHeight += _On_New_Height;
		gameOverRect = GetNode<ColorRect>("GameOverRect");
		scoreLabel = GetNode<Label>("MC/ScoreLabel");
		highScoreLabel = GetNode<Label>("MC/HighScoreLabel");
		gameOverRect.Visible = false;
		highScoreLabel.Text = $"Best: {ScoreManager.highScore:D4}";
	}

	public void _On_Game_Over() {
		gameOverRect.Visible = true;
		GetTree().Paused = true;
	}
	
	public void _On_New_Height(int height) {
		scoreLabel.Text = height.ToString("D4");
	}
	
	public override void _ExitTree() {
		SignalHub.Instance.GameOver -= _On_Game_Over;
		SignalHub.Instance.NewHeight -= _On_New_Height;
	}
}
