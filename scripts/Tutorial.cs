using Godot;

public partial class Tutorial : Control
{
    [Export] private Label[] explanation = null!; 
    [Export] private PackedScene gameScene = null!;

    private int currentIndex = 0;

    public override void _Ready()
    {
        UpdateLabelsVisibility();
    }

    public void _on_next_pressed()
    {
        GD.Print("WHAAAAA");
        if (currentIndex == explanation.Length - 1)
        {
            SwitchScene();
        }
        else
        {
            currentIndex++;
            UpdateLabelsVisibility();
        }
    }

    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.Escape))
        {
            GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
        }
    }

    private void UpdateLabelsVisibility()
    {
        for (int i = 0; i < explanation.Length; i++)
        {
            explanation[i].Visible = (i == currentIndex);
        }
    }

    private void SwitchScene()
    {
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }
}
