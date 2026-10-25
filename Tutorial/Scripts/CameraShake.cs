using Godot;
using System;

public partial class CameraShake : Camera3D
{
	public double Intensity = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		(GetParent() as player_controller).OnTakeDamage += DamageShake;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Intensity > 0)
		{
			Intensity = Mathf.Lerp(Intensity, 0.0, delta * 10);
			Vector2 offset = GetRandomOffset();
			HOffset = offset.X;
			VOffset = offset.Y;
		}
	}

	public Vector2 GetRandomOffset()
	{
		double x = GD.RandRange(-Intensity, Intensity);
		double y = GD.RandRange(-Intensity, Intensity);
		return new Vector2((float)x, (float)y);
	}

	public void DamageShake(int health)
	{
		Intensity = 0.1f;
	}
}
