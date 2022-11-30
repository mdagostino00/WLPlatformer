using Godot;
using System;
using WarioLandPlatformer.PlayerFSM;

public partial class Player : CharacterBody2D
{
	public const float SprintSpeed = 150.0f;
	public const float MaxSpeed = 100.0f;
	public const float JumpVelocity = -350.0f;
	public const float Acceleration = 10.0f;

	public PlayerFSM? playerFSM = null;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// initialization for the player's finite state machine object
	public Player()
	{
		// first, create a new instance of the FSM object
		playerFSM = new PlayerFSM();

		// next, add all of our coded states into the FSM
		playerFSM.Add(new PlayerFSMState_MOVEMENT(this));
        playerFSM.Add(new PlayerFSMState_ATTACK(this));

		playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
    }

	public override void _PhysicsProcess(double delta)
	{
        playerFSM._PhysicsProcess(delta);
        MoveAndSlide();
    }

	public void SetAnimation(Vector2 velocity)
	{
		var animatedSprite = GetNode<AnimatedSprite2D>("PlayerSprite");
		if (IsOnFloor() && velocity.x != 0)

        {
            animatedSprite.Play("walking");
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

		if (velocity.x != 0)
		{
            animatedSprite.FlipH = velocity.x < 0;
        }
	}

	public void OnFallzoneBodyEntered(Node body)
	{
        GetTree().ChangeSceneToPacked((PackedScene)ResourceLoader.Load("res://assets/scenes/Main.tscn"));
	}
}
