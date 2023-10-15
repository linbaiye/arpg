using Godot;
using System;

public partial class Grass : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void CreateGrassEffect()
	{
		PackedScene grassEffectScene = ResourceLoader.Load<PackedScene>("res://Effects/GrassEffect.tscn");
		var grassEffect = (GrassEffect)grassEffectScene.Instantiate();
		var world = GetTree().CurrentScene;
		world.AddChild(grassEffect);
		grassEffect.GlobalPosition = GlobalPosition;

	}

	private void OnHurtboxEntered(Area2D area)
	{
		CreateGrassEffect();
		QueueFree();
		// Replace with function body.
	}
}


