using Godot;
using System;

public partial class Menu : Control
{
	private void OnPlayButtonPressed()
	{
		PlayerStats.Instance.Score = 0;
		GetTree().ChangeSceneToFile("res://Tutorial/Scenes/level_1.tscn");
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
