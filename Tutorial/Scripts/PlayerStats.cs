using Godot;
using System;

public partial class PlayerStats : Node
{
	public static PlayerStats Instance { get; private set; }
	public int Score = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
}
