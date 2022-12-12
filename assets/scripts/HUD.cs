using Godot;
using System;

public partial class HUD : CanvasLayer
{
    Label coins;
    Label orbs;
    Label escapetimer;
    Panel youwin;
    Panel youlose;
    TextureRect key;
    public int _coin = 0;
    public int _orb = 0;

    public override void _Ready()
    {
        coins = GetNode<Label>("Coins");
        orbs = GetNode<Label>("Orbs");
        escapetimer = GetNode<Label>("EscapeTimer");
        key = GetNode<TextureRect>("Key");
        youwin = GetNode<Panel>("YouWin");
        youlose = GetNode<Panel>("YouLose");

        this.Visible = true;
        coins.Text = GD.Str(_coin);
        orbs.Text = GD.Str(_orb);
        escapetimer.Visible = false;
        key.Visible = false;
        youwin.Visible = false;
        youlose.Visible = false;
    }

    void OnPlayerChangedCoins(int coincount)
    {
        coins.Text = GD.Str(coincount);
    }

    void OnPlayerChangedOrbs(int orbcount)
    {
        orbs.Text = GD.Str(orbcount);
    }

    void OnPlayerKeyObtained()
    {
        escapetimer.Visible = true;
        key.Visible = true;
    }

    void OnLevelEnd()
    {
        youwin.Visible = true;
        GetTree().Paused = true;
    }

    void OnLevelTimerUpdate(string timer)
    {
        escapetimer.Text = GD.Str(timer);
    }

    void OnLevelTimerYouLose()
    {
        youlose.Visible = true;
        GetTree().Paused = true;
    }

    void OnPlayAgainButtonUp()
    {
        GetTree().ChangeSceneToFile("res://assets//scenes//Main.tscn");
        GetTree().Paused = false;
    }
}
