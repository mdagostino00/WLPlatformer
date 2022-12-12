using Godot;
using System;

public partial class Key : Treasure
{
    Timer timer;
    public AnimationPlayer animationPlayer;
    [Signal] public delegate void KeyObtainedEventHandler();

    public override void _Ready()
	{
        timer = GetNode<Timer>("Timer");

        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("idle");
        base._Ready();
    }

    public override async void OnBodyEntered(Player body)
    {
        GetTree().Paused = true;
        animationPlayer.Play("bounce");
        ZIndex = 2;
        SetCollisionMaskValue(1, false); //disable collision box
        player = body;

        timer.Start();
        await ToSignal(timer, "timeout");
        EmitSignal("KeyObtained");
    }

    public void OnAnimationPlayerAnimationFinished(string anim_name)
    {
        GetTree().Paused = false;
        this.QueueFree();
    }

    public void OnTimerTimeout()
    {
        GetTree().Paused = false;
        timer.QueueFree();
    }
}
