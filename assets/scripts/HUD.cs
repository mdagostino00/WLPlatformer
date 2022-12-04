using Godot;
using System;

public partial class HUD : CanvasLayer
{
    Label coins;
    Label orbs;
    public int _coin = 0;
    public int _orb = 0;

    public override void _Ready()
    {
        coins = GetNode<Label>("Coins");
        orbs = GetNode<Label>("Orbs");

        coins.Text = GD.Str(_coin);
        orbs.Text = GD.Str(_orb);
    }

    void OnPlayerChangedCoins(int coincount)
    {
        coins.Text = GD.Str(coincount);
    }

    void OnPlayerChangedOrbs(int orbcount)
    {
        orbs.Text = GD.Str(orbcount);
    }
}
