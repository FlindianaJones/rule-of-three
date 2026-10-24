using Godot;
using System;


public partial class Coin : Area3D
{
	[Export]
	public float RotateSpeed = 180f;

	[Export]
	public float BobHeight = 0.2f;

	[Export]
	public float BobSpeed = 5.0f;

	private float StartYPos;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartYPos = Position.Y;
	} 

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Put a little y-spin on it
		Rotation = new(Rotation.X, (float)(Rotation.Y + Mathf.DegToRad(RotateSpeed) * delta), Rotation.Z);

		// Use a sine wave to do a jaunty little bob
		double currentTime = Time.GetUnixTimeFromSystem();
		double yPos = (1 + Mathf.Sin(currentTime * BobSpeed)) / 2 * BobHeight;
		GlobalPosition = new(GlobalPosition.X, StartYPos + (float)yPos, GlobalPosition.Z);
	}

	private void OnBodyEntered(Node3D body)
	{
		if(!body.IsInGroup("Player"))
		{ 
			return;
		}
		((player_controller)body).IncreaseScore(1);
		QueueFree();
	}
}
