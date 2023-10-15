using Godot;
using System;

public partial class GrassEffect : Node2D
{
	private AnimatedSprite2D sprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = (AnimatedSprite2D)GetNode("AnimatedSprite");
		sprite.Frame = 0;
		sprite.Play("Animate");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnAnimationFinished()
	{
		QueueFree();
		// Replace with function body.
	}

}





