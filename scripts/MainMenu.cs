using Godot;
using System;

public partial class MainMenu : Node2D
{
    [Export] private string GameScene = null!;
    [Export] private string Tutorial = null!;

    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {

    }

    public void _on_start_pressed()
    {
        GetTree().ChangeSceneToFile(GameScene);
    }

    public void _on_tutorial_pressed()
    {
        GetTree().ChangeSceneToFile(Tutorial);
    }


    public void _on_quit_pressed()
    {
        GetTree().Quit();
    }
}
