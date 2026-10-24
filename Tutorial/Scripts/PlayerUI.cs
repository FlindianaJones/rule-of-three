using Godot;
using System;

public partial class PlayerUI : CanvasLayer
{
	HBoxContainer HealthContainer;
	// My Vibe is that we need this really specific type because there's another "Collections" that overrules Godot's
	Godot.Collections.Array<Node> Hearts = [];
	Label ScoreText;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player_controller player = GetParent<player_controller>();
		player.OnTakeDamage += UpdateHearts;
		player.OnUpdateScore += UpdateScoreText;

		HealthContainer = GetNode<HBoxContainer>("HealthContainer");
		ScoreText = GetNode<Label>("ScoreText");
		UpdateScoreText(PlayerStats.Instance.Score);

		Hearts = HealthContainer.GetChildren();
		UpdateHearts(player.health);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateHearts(int health)
	{
		for(int i = 0; i < Hearts.Count; i++)
		{
			((TextureRect)Hearts[i]).Visible = i < health;
		}
	}

	public void UpdateScoreText(int score)
	{
		ScoreText.Text = "Score: " + score.ToString();
	}
}
