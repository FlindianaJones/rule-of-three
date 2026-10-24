using Godot;
using System;

public partial class Enemy : Area3D
{
	[Export]
	public float MoveSpeed = 2.0f;

	[Export]
	public Vector3 MoveDirection;

	[Export]
	public float SpinSpeed = 900.0f;

	Vector3 StartPos;
	Vector3 TargetPos;
	Node3D Model;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartPos = GlobalPosition;
		TargetPos = StartPos + MoveDirection;
		Model = GetNode<Node3D>("Model");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector3 newRotation = new(Model.Rotation.X, Model.Rotation.Y, (float)(Model.Rotation.Z + Mathf.DegToRad(SpinSpeed) * delta));
		Model.Rotation = newRotation;
		GlobalPosition = GlobalPosition.MoveToward(TargetPos, MoveSpeed * (float)delta);
		if (GlobalPosition == StartPos)
		{
			TargetPos = StartPos + MoveDirection;
		}
		if (GlobalPosition == StartPos + MoveDirection)
		{
			TargetPos = StartPos;
		}
	}

	private void OnBodyShapeEntered(Node3D body)
	{
		if(!body.IsInGroup("Player"))
		{ 
			return;
		}
		(body as player_controller).TakeDamage(1);
	}
}
