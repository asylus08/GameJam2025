using Godot;
using System;

public partial class Gameover : Node2D
{
	[Export] private PackedScene menu = null!;

   public void _on_return_menu_pressed()
	{
		GetTree().ChangeSceneToPacked(menu);
	}
}
