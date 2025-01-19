using Godot;

public partial class Tooltip : TextureRect
{
	private Label _labelCommoner;
	private Label _labelClergy;
	private Label _labelArmy;
	
	public override void _Ready()
	{
		_labelCommoner = GetNode<Label>("ScoreCommoner");
		_labelClergy = GetNode<Label>("ScoreClergy");
		_labelArmy = GetNode<Label>("ScoreArmy");

	}

	public void SetScores(int scoreCommoner, int scoreClergy, int scoreArmy)
	{
		_labelCommoner.Text = (scoreCommoner > 0 ? "+": "") + scoreCommoner;
		_labelClergy.Text = (scoreClergy > 0 ? "+": "") + scoreClergy;
		_labelArmy.Text = (scoreArmy > 0 ? "+": "") + scoreArmy;
	}


}
