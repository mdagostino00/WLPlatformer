using Godot;
using System;

public partial class Treasure : Area2D
{
    [Export] public int TREASURE_VALUE = 500;

    public virtual void OnBodyEntered(Node2D body)
    {
        this.QueueFree();
    }
}
