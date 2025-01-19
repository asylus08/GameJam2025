using Godot;

namespace GamejamV2.scripts;
public partial class WordButton : TextureButton
{
	[Export] private GameScene _game = null!;
	[Export] public string word = "";
	public Label text = null!;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		text = GetNode<Label>("Label");
        text.Text = word;
        GD.Print(word);
	}

	public override void _Pressed()
	{
		_game.ClickWord(word);
    }
}
