using Godot;
using System;

public partial class OrbTreasure : Treasure
{
    public AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("idle");
        base._Ready();
    }

    public override void OnBodyEntered(Player body)
    {
        animationPlayer.Play("bounce");
        player = body;  
    }

    public void OnAnimationPlayerAnimationFinished(string anim_name)
    {
        player.AddOrb();
        this.QueueFree();
    }
}
