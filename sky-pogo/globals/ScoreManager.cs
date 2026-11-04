using Godot;
using System;

public partial class ScoreManager : Node {
	
	private readonly String SAVE_PATH = "user://sky_enc.cfg";
	private readonly String SECTION = "game";
	private readonly String VALUE_KEY = "highscore";
	private readonly String PW = "abcdef";
		
	
	public static int highScore { get; private set; }
	
	public override void _Ready() {
		SignalHub.Instance.NewHeight += _On_New_Height;
		SignalHub.Instance.GameOver += _On_Game_Over;
	}

	public override void _EnterTree() {
		loadScore();
	}

	private void _On_Game_Over() {
		saveScore();
	}

	private void _On_New_Height(int height) {
		if (height > highScore) {
			highScore = height;
		}
	}

	private void saveScore() {
		ConfigFile config = new ConfigFile();
		config.SetValue(SECTION, VALUE_KEY, highScore);
		config.SaveEncryptedPass(SAVE_PATH, PW);
	}

	private void loadScore() {
		ConfigFile config = new ConfigFile();
		if (config.LoadEncryptedPass(SAVE_PATH, PW) == Error.Ok) {
			highScore = (int)config.GetValue(SECTION, VALUE_KEY, 0);
		}
	}
}
