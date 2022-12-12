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
	public CollisionShape2D _pickHitbox;
	public Sprite2D _animatedSprite;
    public AudioStreamPlayer2D _hitstunsfx;
    public AudioStreamPlayer2D _jumpsfx;

    // Gameplay-related variables
    public int coins { get; set; }
	public int orbs { get; set; }	
    public bool animationActable { get; set; }
	public bool inputActionable { get; set; }


	// Signals
	[Signal] public delegate void ChangeCoinsEventHandler(int coins);
    [Signal] public delegate void ChangeOrbsEventHandler(int orbs);
    [Signal] public delegate void OnPlayerEnteredHiddenEventHandler(Player body);


    // initialization for the player's finite state machine object
    public Player()
	{
		// first, create a new instance of the FSM object
		playerFSM = new PlayerFSM();

		// next, add all of our coded states into the FSM
		playerFSM.Add(new PlayerFSMState_MOVEMENT(this));
        playerFSM.Add(new PlayerFSMState_ATTACK(this));
        playerFSM.Add(new PlayerFSMState_HITSTUN(this));

        playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);

		playerObserver = new PlayerObserver();
    }

	//-----------------Godot Engine Functions---------------------//

	public override void _Ready()
	{
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _animatedSprite = GetNode<Sprite2D>("PlayerSpriteSheet");

        _pickHitbox = (CollisionShape2D)GetNode<Area2D>("PickaxeHitbox").GetChild(0);
        _pickHitbox.SetDeferred("Disabled", true);

        _hitstunsfx = GetNode<AudioStreamPlayer2D>("HitstunSFX");
        _jumpsfx = GetNode<AudioStreamPlayer2D>("JumpSFX");

        inputActionable = true;
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

    //-----------------Input Handling---------------------//

    public Vector2 GetInputDirection()
	{
        Vector2 direction = Input.GetVector(
			"move_left",
			"move_right",
			"move_up",
			"move_down"
			);
		if (!inputActionable)
		{
			direction.x = 0;
			direction.y = 0;
		}
		return direction;
    }

	public void SetAnimation(Vector2 velocity)
	{ 
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
			if (_animatedSprite.FlipH = velocity.x < 0)
			{
                _animatedSprite.Offset = new Vector2(-32.0f , 0.0f);
			}else
			{
                _animatedSprite.Offset = new Vector2(0.0f, 0.0f);
            }
        }
	}

    //-----------------Called Functions in other scripts---------------------//

    public void TriggerKnockback(Vector2 enemypos)
	{
        Vector2 velocity = new Vector2(200f, Player.JumpVelocity *.6f);
        if (Position.x < enemypos.x)
            velocity.x = -200f;
        Velocity = velocity;
        _hitstunsfx.Play();

        playerFSM.SetCurrentState(PlayerFSMStateType.HITSTUN);

    }

	public void TriggerBounceOffEnemy()
	{
        _jumpsfx.Play();
		Velocity = new Vector2(Velocity.x, JumpVelocity * .8f);
	}

    public void AddOrb()
    {
        orbs += 1;
        EmitSignal("ChangeOrbs", orbs);
    }

    public void AddCoins(int coin)
    {
        coins += coin;
        EmitSignal("ChangeCoins", coins);
    }

    public void SetAnimationActable()
    {
        animationActable = true;
    }

    public void ActivateHitbox()
    {
        Vector2 hitboxPos = new Vector2(25, -8);
        if (_animatedSprite.FlipH)
        {
            hitboxPos.x = -25;
        }

        _pickHitbox.Position = hitboxPos;
        _pickHitbox.Disabled = false;
    }

    public void DeactivateHitbox()
    {
        _pickHitbox.Disabled = true;
    }

    //-----------------Signal Handling Functions---------------------//


    public void OnFallzoneBodyEntered(Player body)
	{
        GetTree().ChangeSceneToFile("res://assets/scenes/Main.tscn");
    }


	public void OnAnimationPlayerAnimationFinished(string anim_name)
	{
		if (anim_name == "attack")
		{
            playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        }else if (anim_name == "hitstun")
		{
            playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        }
	}
}
