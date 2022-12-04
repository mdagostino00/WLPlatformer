using Godot;
using System;
using WarioLandPlatformer;
using WarioLandPlatformer.PlayerFSM;

public partial class Player : CharacterBody2D
{
	// Physics Variables
	public const float SprintSpeed = 150.0f;
	public const float MaxSpeed = 100.0f;
	public const float JumpVelocity = -350.0f;
	public const float Acceleration = 10.0f;
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Composite Objects
	public AnimationPlayer _animationPlayer;
	public PlayerFSM playerFSM = null;
	public PlayerObserver playerObserver = null;

	// Gameplay-related variables
    public int coins { get; set; }
	public int orbs { get; set; }	
    public bool animationActable { get; set; }


	// Signals
	[Signal] public delegate void ChangeCoinsEventHandler(int coins);
    [Signal] public delegate void ChangeOrbsEventHandler(int orbs);


    // initialization for the player's finite state machine object
    public Player()
	{
		// first, create a new instance of the FSM object
		playerFSM = new PlayerFSM();

		// next, add all of our coded states into the FSM
		playerFSM.Add(new PlayerFSMState_MOVEMENT(this));
        playerFSM.Add(new PlayerFSMState_ATTACK(this));

		playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);

		playerObserver = new PlayerObserver();
    }

	public override void _Ready()
	{
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

	public override void _Process(double delta)
	{
		playerFSM._Process(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
        playerFSM._PhysicsProcess(delta);
        MoveAndSlide();
    }

	public void SetAnimation(Vector2 velocity)
	{
		var animatedSprite = GetNode<Sprite2D>("PlayerSpriteSheet");
		if (IsOnFloor() && velocity.x != 0)

        {
			_animationPlayer.Play("walking");
        }
		else if (!IsOnFloor())
		{
			if (velocity.y < 0)
                _animationPlayer.Play("jumping_up");
			else
                _animationPlayer.Play("jumping_down");
        }
		else
		{
            //_animationPlayer.Stop();
            _animationPlayer.Play("idle");
		}

		if (velocity.x != 0)
		{
			if (animatedSprite.FlipH = velocity.x < 0)
			{
                animatedSprite.Offset = new Vector2(-32.0f , 0.0f);
			}else
			{
                animatedSprite.Offset = new Vector2(0.0f, 0.0f);
            }
        }
	}

	public void OnFallzoneBodyEntered(Node body)
	{
        GetTree().ChangeSceneToPacked((PackedScene)ResourceLoader.Load("res://assets/scenes/Main.tscn"));
	}


	public void OnAnimationPlayerAnimationFinished(string anim_name)
	{
		if (anim_name == "attack")
		{
            playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        }
	}

    public void AddOrb()
    {
        orbs += 1;
		EmitSignal("ChangeOrbs", orbs);
		GD.Print(orbs);
    }

    public void AddCoins(int coin)
	{
		coins += coin;
		EmitSignal("ChangeCoins", coins);
		GD.Print(coins);
	}



    public void SetAnimationActable()
    {
		animationActable = true;
    }

    public void ActivateHitbox()
	{

	}
}
