using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
	[Export]
	public float MoveSpeed = 3f;

	[Export]
	public float JumpForce = 8f;

	[Export]
	public float Gravity = 20f;

	[Export]
	public Camera3D Camera = null;

	[Export]
	public int health = 3;

	[Export]
	public float LookSpeed = 0.01f;

	[Signal] // These also have to end in "EventHandler" in order for the magic to happen
	public delegate void OnTakeDamageEventHandler(int hp);

	public AudioStreamPlayer Audio;
	public readonly AudioStream CoinSFX = ResourceLoader.Load<AudioStream>("res://Tutorial/Audio/coin.wav");

	public readonly AudioStream TakeDamageSFX = ResourceLoader.Load<AudioStream>("res://Tutorial/Audio/take_damage.wav");
	public Node3D Pivot;
    public override void _Ready()
    {
        Audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		Pivot = GetNode<Node3D>("Pivot");
		Input.MouseMode = Input.MouseModeEnum.ConfinedHidden;
    }


    public override void _Process(double delta)
    {
        if (GlobalPosition.Y < -5)
		{
			GameOver();
		}
    }

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventMouseMotion eventMouseMotion && Input.MouseMode != Input.MouseModeEnum.Visible)
		{
			this.GlobalRotate(Vector3.Down, Mathf.DegToRad(LookSpeed * eventMouseMotion.ScreenVelocity.X));
			Pivot.Rotate(Vector3.Right, Mathf.DegToRad(LookSpeed * eventMouseMotion.ScreenVelocity.Y));
		}
		else if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.ButtonIndex == MouseButton.Left)
		{
			if (Input.MouseMode != Input.MouseModeEnum.ConfinedHidden)
			{
				Input.MouseMode = Input.MouseModeEnum.ConfinedHidden;
			}
		}
    }

    public override void _UnhandledKeyInput(InputEvent @event)
	{
		if(Input.IsActionJustPressed("escape_mouse") && Input.MouseMode == Input.MouseModeEnum.ConfinedHidden)
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}


	public override void _PhysicsProcess(double delta)
	{
		// TODO: need to dramatically alter how controls work here, so we're not fixed camera slumming it up
		// Apply gravity
		double VerticalSpeed = Velocity.Y - Gravity * delta;
		// Implement Jump
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			VerticalSpeed = JumpForce;
		}
		// Implement Movement
		Vector2 MoveInput =  Input.GetVector("strafe_right", "strafe_left", "move_back", "move_forward").Normalized();
		Vector3 MoveDir = new Vector3(MoveInput.X * MoveSpeed * (float)delta, 0, MoveInput.Y * MoveSpeed * (float)delta);
		TranslateObjectLocal(MoveDir);
		Velocity = new(Velocity.X, (float)VerticalSpeed, Velocity.Z);

		MoveAndSlide();
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
		PlaySound(TakeDamageSFX);
		EmitSignal(SignalName.OnTakeDamage, health);
		if (health <=0)
		{
			GameOver();
		}
	}

	public void GameOver()
	{
		Input.MouseMode = Input.MouseModeEnum.Visible;
		Callable.From(() => { GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn"); }).CallDeferred();
	}


	public void PlaySound(AudioStream sound)
	{
		Audio.Stream = sound;
		Audio.Play();
	}
}
