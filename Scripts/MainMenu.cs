using Godot;
using System;

public partial class MainMenu : Control
{	
	private void OnPlayButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/DemoLevel.tscn");
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
