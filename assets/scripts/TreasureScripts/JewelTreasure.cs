using Godot;
using System;

public partial class JewelTreasure : Treasure
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
        SetCollisionMaskValue(1, false); //disable collision box
        player = body;
    }

    public void OnAnimationPlayerAnimationFinished(string anim_name)
    {
        base.OnBodyEntered(player);
    }
}
