using Godot;
using System;

public partial class Treasure : Area2D
{
    protected Player player;
    [Export] public int TREASURE_VALUE = 500;

    public virtual void OnBodyEntered(Player body)
    {
        body.AddCoins(TREASURE_VALUE);
        this.QueueFree();
        //GD.Print(body.GetType());
    }

    public virtual void OnBodyEntered(Node2D body)
    {

    }
}
