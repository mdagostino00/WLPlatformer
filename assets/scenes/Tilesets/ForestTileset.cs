using Godot;
using System;

public partial class ForestTileset : TileMap
{
	public void ToggleHiddenVisibility()
	{
		this.SetLayerModulate(3, new Color(0,0,0,0));
	}
}
