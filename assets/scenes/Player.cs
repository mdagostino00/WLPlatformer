using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float SprintSpeed = 150.0f;
	public const float MaxSpeed = 100.0f;
	public const float JumpVelocity = -300.0f;
	public const float Acceleration = 10.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.y = JumpVelocity;
		if (Input.IsActionJustReleased("jump") && velocity.y < 0)
			velocity.y = 0;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction.x != 0)
		{
			/*
			if (Input.IsActionPressed("sprint"))
			{
				velocity.x += direction.x * Acceleration;
				velocity.x = Mathf.Clamp(velocity.x, -SprintSpeed, SprintSpeed);
			}
			else
			{
				velocity.x = direction.x * MaxSpeed;
			}
			*/
			velocity.x += direction.x * Acceleration;
            velocity.x = Mathf.Clamp(velocity.x, -MaxSpeed, MaxSpeed);
        }
		else
		{
			velocity.x = Mathf.MoveToward(Velocity.x, 0, Acceleration);
		}

		Velocity = velocity;
		MoveAndSlide();
		SetAnimation(Velocity);
	}

	public void SetAnimation(Vector2 velocity)
	{
		var animatedSprite = GetNode<AnimatedSprite2D>("PlayerSprite");
		
		if (IsOnFloor() && velocity.x != 0)
		{
            animatedSprite.Play("walking");
            animatedSprite.FlipH = velocity.x < 0;
        }
		else if (!IsOnFloor())
		{
			if (velocity.y < 0)
				animatedSprite.Play("jumping_up");
			else
				animatedSprite.Play("jumping_down");
		}
		else
		{
			animatedSprite.Play("idle");
		}
	}
}
