using Godot;
using System;

public partial class Enemy : CharacterBody2D
{ 
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	[Export] public int direction = -1;
	[Export] public int SPEED = 20;
	[Export] public bool detectCliffs = true;

	protected Sprite2D _sprite;
	protected AnimationPlayer _animationPlayer;
	protected Area2D _hurtbox;
	protected RectangleShape2D _hurtboxShape;
	protected RayCast2D _rayCast;
	protected Area2D _hitbox;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        _hurtbox = GetNode<Area2D>("Hurtbox");
        _hurtboxShape = (RectangleShape2D)GetNode<CollisionShape2D>("Hurtbox/HurtboxShape").Shape;
		_hitbox = GetNode<Area2D>("Hitbox");

		_rayCast = GetNode<RayCast2D>("FloorChecker");

		Vector2 rayCastVector = new Vector2(_hurtboxShape.Size.x * direction, 0);
		_rayCast.Position = rayCastVector;
		_rayCast.Enabled = detectCliffs;


		if (!detectCliffs)
			Modulate = new Color(0,1,0.2f);

		_animationPlayer.Play("walk");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        UpdateAnimation();
    }

	public override void _PhysicsProcess(double delta)
	{
        Vector2 velocity = this.Velocity;
		if (IsOnWall() || !_rayCast.IsColliding() && detectCliffs && IsOnFloor())
		{
			direction = direction * -1;
		}

		if (!IsOnFloor()) 
			velocity.y += gravity * (float)delta;
		velocity.x = SPEED * direction;
        Velocity = velocity;

		
        MoveAndSlide();
        UpdateFloorRaycast();
    }

	public void UpdateAnimation()
	{
		if (direction == 1)
		{
			_sprite.FlipH = true;
		}
		else
		{
			_sprite.FlipH = false;
		}

	}

	public void UpdateFloorRaycast()
	{
        _rayCast.Position = new Vector2(_hurtboxShape.Size.x * 0.5f * direction, -5);
    }

	public void OnHurtboxAreaEntered(Area2D area)
	{
        SetCollisionLayerValue(5, false);
        SetCollisionMaskValue(1, false);
        _hurtbox.SetCollisionLayerValue(5, false);
        _hurtbox.SetCollisionMaskValue(1, false);
        _hitbox.SetCollisionLayerValue(5, false);
        _hitbox.SetCollisionMaskValue(1, false);

        _animationPlayer.Play("kill");
        SPEED = 0;
    }

	public void OnHurtboxBodyEntered(Player body)
	{
		SetCollisionLayerValue(5, false);
		SetCollisionMaskValue(1, false);
		_hurtbox.SetCollisionLayerValue(5, false);
        _hurtbox.SetCollisionMaskValue(1, false);
        _hitbox.SetCollisionLayerValue(5, false);
        _hitbox.SetCollisionMaskValue(1, false);

		body.TriggerBounceOffEnemy();
        _animationPlayer.Play("kill");
		SPEED = 0;
	}

	public void OnHitboxBodyEntered(Player body)
	{
		GD.Print("Hitbox entered!");
		body.TriggerKnockback(this.Position);
    }

    public void OnHitboxAreaEntered(Area2D area)
	{

	}

    public void OnAnimationPlayerAnimationFinished(string anim_name)
	{
		this.QueueFree();
	}
}
