using Godot;
using System;

public enum State
{
	MOVE,
	ROLL,
	ATTACK,
}

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private const float ACCELARIONT = 500;
	private const float MAX_SPEED = 80;
	private const float FRICTION = 500;
	private State state = State.MOVE;
	
	private Vector2 rollVector = Vector2.Right;


	// Get the gravity from the project settings to be synced with RigidBody nodes.
	// public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public float gravity = 0;

	public Player()
	{
		//var animationTree = (AnimationTree)GetNode("AnimationTree");
		//animationTree.Active = true;

	}

	public override void _PhysicsProcess(double delta)
	{
		var animationTree = (AnimationTree)GetNode("AnimationTree");
		animationTree.Active = true;
		switch (state)
		{
			case State.MOVE:
				MoveState(delta);
				break;
			case State.ATTACK:
				AttackState(delta);
				break;
			case State.ROLL:
				RollState(delta);
				break;
		}
	}

	private void MoveState(double delta)
	{
		Vector2 inputVector = Vector2.Zero;
		inputVector.X = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		inputVector.Y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		inputVector.Normalized();
		//		Vector2 velocity = new((float)(inputVector.X * delta * 100), (float)(inputVector.Y * delta * 100));
		//Vector2 velocity = new((float)(inputVector.X  * 100), (float)(inputVector.Y  * 100));
		var animationTree = (AnimationTree)GetNode("AnimationTree");
		var playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		if (!inputVector.Equals(Vector2.Zero))
		{
			animationTree.Set("parameters/Idle/blend_position", inputVector);
			animationTree.Set("parameters/Run/blend_position", inputVector);
			animationTree.Set("parameters/Attack/blend_position", inputVector);
			animationTree.Set("parameters/Roll/blend_position", inputVector);
			playback.Travel("Run");
			Velocity = Velocity.MoveToward(inputVector * MAX_SPEED, (float)(ACCELARIONT * delta));
			rollVector = inputVector;
		}
		else
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, (float)(FRICTION * delta));
			playback.Travel("Idle");
		}
		MoveAndSlide();
		if (Input.IsActionJustPressed("Attack"))
		{
			state = State.ATTACK;
		}
		if (Input.IsActionJustPressed("Roll"))
		{
			state = State.ROLL;
		}

	}

	private void AttackState(double delta)
	{
		Velocity = Vector2.Zero;
		var animationTree = (AnimationTree)GetNode("AnimationTree");
		var playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		playback.Travel("Attack");
	}

	private void RollState(double state)
	{
		Velocity = rollVector * (MAX_SPEED * 1.2f);
		MoveAndSlide();
		var animationTree = (AnimationTree)GetNode("AnimationTree");
		var playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		playback.Travel("Roll");
	}
	
	public void AttackFinished()
	{
		state = State.MOVE;
	}

	public void RollFinished()
	{
		state = State.MOVE;
	}
}
