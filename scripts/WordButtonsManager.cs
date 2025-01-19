using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamejamV2.scripts;

public partial class WordButtonsManager : HBoxContainer
{
    private static WordButton[] wordButtons;
    private static List<string> wordsInUse = new List<string>();

	[Signal]
	public delegate void HappyClergyEventHandler();

	[Signal]
	public delegate void HappyCommonerEventHandler();

    [Signal]
    public delegate void HappyArmyEventHandler();
    
    [Signal]
    public delegate void NeutralClergyEventHandler();

    [Signal]
    public delegate void NeutralCommonerEventHandler();

    [Signal]
    public delegate void NeutralArmyEventHandler();

    [Signal]
    public delegate void SadClergyEventHandler();

	[Signal]
	public delegate void SadCommonerEventHandler();

	[Signal]
	public delegate void SadArmyEventHandler();


	[Export] private GameScene _game = null!;
	[Export] private bool _showTooltip = true;
	private Tooltip _tooltip = null!;
	private int scoreTotal = 0;
	private int fullScore = 125;
	private int scoreClergy = 0;
	private int scoreCommoner = 0;
	private int scoreArmy = 0;
	public override void _Ready()
	{
		_tooltip = GetNode<Tooltip>("%Tooltip");
		wordButtons = GetChildren().OfType<WordButton>().ToArray();
		SetWordButtons();
		foreach (WordButton wordButton in wordButtons)
		{
			wordButton.ButtonDown += CalculateScore;
		}
	}
    public void CalculateScore()
    {
        fullScore += this.scoreTotal;
        _game.UpdateScores(fullScore, this.scoreClergy, this.scoreCommoner, this.scoreArmy);
    }
    public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Escape))
		{
			GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
		}

		if (_showTooltip && Array.Exists(wordButtons, element => element.IsHovered() && !element.Disabled))
		{
			foreach (WordButton button in wordButtons)
				if (button.IsHovered() && !button.Disabled)
				{
					_tooltip.Visible = true;
					this.scoreCommoner = Words.words[button.word].commoner *
										 Sentences.sentences[_game.currentSentence].commoner;
					this.scoreClergy = Words.words[button.word].clergy *
									  Sentences.sentences[_game.currentSentence].clergy;

					int scoreArmy = Words.words[button.word].army * Sentences.sentences[_game.currentSentence].army;
					this.scoreTotal = scoreCommoner + scoreClergy + scoreArmy;
					EvaluateAura(scoreCommoner, scoreClergy, scoreArmy);

					if (button.IsPressed())
					{
						EvaluateGains(scoreCommoner, scoreClergy, scoreArmy);
					}

					_tooltip.SetScores(scoreCommoner, scoreClergy, scoreArmy);
					_tooltip.GlobalPosition = new Vector2(GetGlobalMousePosition().X - _tooltip.Size.X / 2, 800);
				}


			_tooltip.Visible = true;

		} else {
			_tooltip.Visible = false;
			DeactivateAura();
				};
	}

	public void EvaluateAura(int commonerScore, int clergyScore, int armyScore)
	{
        Clergy.score = clergyScore;
        Soldier.score = armyScore;
        Commoner.score = commonerScore;
        
	}

	public void DeactivateAura()
	{
		Clergy.score = 0;
		Soldier.score = 0;
		Commoner.score = 0;
	}

    public void SetButtonState(bool enabled)
	{
		foreach(WordButton button in wordButtons)
		{
			button.Disabled = !enabled;
		}
	}

    public void SetWordButtons()
    {
        foreach (WordButton button in wordButtons)
        {
            while (true)
            {
                string selectedWord = SelectRandomWord();
                bool contains = wordsInUse.Contains(selectedWord);
                if (!contains)
                {
                    wordsInUse.Add(selectedWord);
                    button.word = selectedWord;
                    button.text.Text = button.word;
                    break;
                }
            }
        }

        wordsInUse.Clear();
    }

    public String SelectRandomWord()
    {
        Random rand = new Random();
        List<String> wordKeys = new List<String>(Words.words.Keys);
        String selectedKey = wordKeys[rand.Next(0, wordKeys.Count - 1)];
        return selectedKey;
    }

    private void EvaluateGains(int commonerScore, int clergyScore, int armyScore)
    {
        int happyDemographics = 0;
        if (commonerScore > 0)
        {
            happyDemographics++;
        }

        if (clergyScore > 0)
        {
            happyDemographics++;
        }

        if (armyScore > 0)
        {
            happyDemographics++;
        }

        if (happyDemographics == 0)
        {
            {
                if (clergyScore < commonerScore && clergyScore < armyScore)
                {
                    EmitSignal(SignalName.SadClergy);
                }
                else if (commonerScore < clergyScore && commonerScore < armyScore)
                {
                    EmitSignal(SignalName.SadCommoner);
                }
                else
                {
                    EmitSignal(SignalName.SadArmy);
                }
            }
        }
        
        else if (happyDemographics == 1)
        {
            if (clergyScore > commonerScore && clergyScore > armyScore)
            {
                EmitSignal(SignalName.NeutralClergy);
            }
            else if (commonerScore > clergyScore && commonerScore > armyScore)
            {
                EmitSignal(SignalName.NeutralCommoner);
            }
            else
            {
                EmitSignal(SignalName.NeutralArmy);
            }
        }
        
        else if (happyDemographics == 2)
        {
            if (clergyScore > commonerScore && clergyScore > armyScore)
            {
                EmitSignal(SignalName.HappyClergy);
            }
            else if (commonerScore > clergyScore && commonerScore > armyScore)
            {
                EmitSignal(SignalName.HappyCommoner);
            }
            else
            {
                EmitSignal(SignalName.HappyArmy);
            }
        }
    }
}