using Godot;
using Microsoft.VisualBasic;
using System;

public partial class player_controller : CharacterBody3D
{
	[Export]
	public float MoveSpeed = 3.0f;

	[Export]
	public float JumpForce = 8.0f;

	[Export]
	public float Gravity = 20.0f;

	[Export]
	public Camera3D Camera = null;

	[Export]
	public int health = 3;

	[Signal] // These also have to end in "EventHandler" in order for the magic to happen
	public delegate void OnTakeDamageEventHandler(int hp);

	[Signal] // These have to be public, or you can't event subscribe to them!
	public delegate void OnUpdateScoreEventHandler(int score);

    public override void _Process(double delta)
    {
        if (GlobalPosition.Y < -5)
		{
			GameOver();
		}
    }


	public override void _PhysicsProcess(double delta)
	{
		// Apply gravity
		double VerticalSpeed = Velocity.Y - Gravity * delta;
		// Implement Jump
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			VerticalSpeed = JumpForce;
		}
		// Implement Movement
		Vector2 MoveInput =  -Input.GetVector("move_right", "move_left", "move_back", "move_forward").Normalized();
		Vector3 MoveDir = new(MoveInput.X * MoveSpeed, (float)VerticalSpeed, MoveInput.Y * MoveSpeed);
		Velocity = MoveDir;

		MoveAndSlide();
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
		EmitSignal(SignalName.OnTakeDamage, health);
		if (health <=0)
		{
			GameOver();
		}
	}

	public void GameOver()
	{
		PlayerStats.Instance.Score = 0;
		GetTree().ReloadCurrentScene();
	}

	public void IncreaseScore(int amount)
	{
		PlayerStats.Instance.Score += amount;
		EmitSignal(SignalName.OnUpdateScore, PlayerStats.Instance.Score);
	}
}
