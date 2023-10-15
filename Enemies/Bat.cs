using Godot;
using System;

public partial class Bat : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private Vector2 knockback = Vector2.Zero;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = 0;

	public override void _PhysicsProcess(double delta)
	{
		knockback = knockback.MoveToward(Vector2.Zero, (float)(50 * delta));
		Velocity = knockback;
		MoveAndSlide();
	}


	private void OnHurtboxEntered(Area2D area)
	{
		//QueueFree();
		knockback = Vector2.Right * 80;
	}

}


