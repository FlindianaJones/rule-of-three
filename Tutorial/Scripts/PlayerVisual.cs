using Godot;
using System;

public partial class PlayerVisual : MeshInstance3D
{
	[Export]
	public double RotateRate = 20.0f;
	double TargetYRot = 0f;
	CharacterBody3D Player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = (CharacterBody3D)GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		SmoothRotation(delta);
		MoveBob(delta);
	}

	public void SmoothRotation(double delta)
	{
		Vector3 vel = -Player.Velocity;

		if (vel.X != 0 || vel.Z != 0)
		{
			TargetYRot = Mathf.Atan2(vel.X, vel.Z);
		}

		Rotation = new(0, (float)Mathf.LerpAngle(Rotation.Y, TargetYRot, RotateRate * delta), 0);
	}

	public void MoveBob(double delta)
	{
		double moveSpeed = Player.Velocity.Length();

		// Only bob when the player is on the floor
		if (moveSpeed < 0.1 || !Player.IsOnFloor())
		{
			Scale = new(Scale.X, 1, Scale.Z);
			return;
		}

		double time = Time.GetUnixTimeFromSystem();
		double yScale = 1 + (Math.Sin(time * 30) * 0.08);
		Scale = new(Scale.X, (float)yScale, Scale.Z);
	}
}
