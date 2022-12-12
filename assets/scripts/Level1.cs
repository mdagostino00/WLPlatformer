using Godot;
using System;

public partial class Level1 : Node
{
	public AudioStreamPlayer level1music;
    public AudioStreamPlayer levelescapemusic;
    public AudioStreamPlayer youwinmusic;
    public AudioStreamPlayer youlosemusic;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		level1music = GetNode<AudioStreamPlayer>("Level1Music");
        levelescapemusic = GetNode<AudioStreamPlayer>("LevelEscapeMusic");
        youwinmusic = GetNode<AudioStreamPlayer>("YouWinMusic");
        youlosemusic = GetNode<AudioStreamPlayer>("YouLoseMusic");

        level1music.Play();
	}

	public void OnPlayerKeyObtained()
	{
		level1music.Stop();
		levelescapemusic.Play();
	}

	public void OnLevelEnd()
	{
        level1music.Stop();
        levelescapemusic.Stop();
		youwinmusic.Play();
	}

	public void OnLevelTimerYouLose()
	{
        level1music.Stop();
        levelescapemusic.Stop();
		youlosemusic.Play();
    }
}
