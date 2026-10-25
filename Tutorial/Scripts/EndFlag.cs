using Godot;
using System;

public partial class EndFlag : Area3D
{
	[Export]
	public PackedScene SceneToLoad;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node3D body)
	{
		if(!body.IsInGroup("Player"))
		{
			return;
		}
		// Deferring this prevents it from causing issues with physics processing while trying to change scene
		Callable.From(() => { GetTree().ChangeSceneToPacked(SceneToLoad); }).CallDeferred();
	
	}
}
