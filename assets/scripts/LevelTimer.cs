using Godot;
using System;

public partial class LevelTimer : Timer
{
    [Export] public int Time = 60;
    [Signal] public delegate void TimerUpdateEventHandler(string time);
    [Signal] public delegate void TimerYouLoseEventHandler(string time);


    private string GetCurrentTime()
    {
        int mins = (int)(Time / 60);
        int secs = Time - (mins * 60);
        return $"{mins:00}:{secs:00}";
    }

    public void OnTimerTimeout()
    {
        Time--;
        EmitSignal("TimerUpdate", GetCurrentTime());
        if (Time == 0)
        {
            EmitSignal("TimerYouLose");
        }
    }

    public void OnPlayerKeyObtained()
    {
        this.Start();
    }
}
