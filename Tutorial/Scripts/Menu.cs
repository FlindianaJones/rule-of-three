using Godot;
using System;

public partial class Menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

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
