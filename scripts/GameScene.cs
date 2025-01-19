using System;
using Godot;

namespace GamejamV2.scripts;
public partial class GameScene : Control
{
	[Export] private string next_level = null!;
	[Export] private string game_over = null!;
	[Export] private int scoreToWin;
	[Export] private GameSentence _sentence = null!;
	[Export] private WordButtonsManager _wordButtonsManager = null!;
	[Export] private bool _doNightCycle;
	[Export] private ColorRect _nightNexture = null!;
	[Export] private Label timeLabel = null!;
	
	private Label _labelScore = null!;
	const int MAX_SCORE = 50;
	private TextureProgressBar _progressBar = null!;
	private Timer _answerTimer = null!;
	private Timer _delayTimer = null!;
	public string currentSentence;
	private string _word;
	private double _currentScore;
	private bool game_ended = false;
	private ProgressBar _armyProgressBar = null!;
	private ProgressBar _cleryProgressBar = null!;
	private ProgressBar _commonerProgressBar = null!;
	private ProgressBar _totalProgressBar = null!;
	private TextureProgressBar _progressTimer = null!;
	private Label _scoreLabel;
	TextureRect _winningMessage;
    //private AnimationPlayer _anim;


    public override void _Ready()
	{
         _winningMessage = GetNode<TextureRect>("%WinningMessage");
		_winningMessage.Visible= false;
        _answerTimer = GetNode<Timer>("%AnswerTimer");
		_delayTimer = GetNode<Timer>("%DelayTimer");
		_armyProgressBar = GetNode<ProgressBar>("%ArmyProgressBar");
		_cleryProgressBar = GetNode<ProgressBar>("%CleryProgressBar");
		_commonerProgressBar = GetNode<ProgressBar>("%CommonerProgressBar");
		_totalProgressBar = GetNode<ProgressBar>("%TotalProgressBar");
		_scoreLabel = GetNode<Label>("%ScoreTotal");
		_progressTimer = GetNode<TextureProgressBar>("%TimerProgress");
		//_anim = GetNode<AnimationPlayer>("%AnimationPlayer");

		_answerTimer.Timeout += OnAnswerComplete;
		_delayTimer.Timeout += OnDelayComplete;
		_labelScore = GetNode<Label>("%ScoreTotal");
		_armyProgressBar.Value = 125;
		_cleryProgressBar.Value = 125;
		_commonerProgressBar.Value = 125;
		_totalProgressBar.Value = 125;
		_scoreLabel.Text = "Score Total: 125";
		OnDelayComplete();
	}

	public override void _Process(double delta)
	{
		if (_doNightCycle)
		{
			_nightNexture.Color = new Color(
				_nightNexture.Color.R,
				_nightNexture.Color.G,
				_nightNexture.Color.B,
				(float)Math.Sin((float)Time.GetTicksMsec() * 0.0005)
				);
		}

		if (_answerTimer.IsStopped() || game_ended)
		{
			timeLabel.Visible = false;
			_progressTimer.Visible = false;
		}
		else
		{
			timeLabel.Visible = true;
			_progressTimer.Visible = true;
			timeLabel.Text = Mathf.Floor(_answerTimer.TimeLeft + 1).ToString();
			_progressTimer.Value = _answerTimer.TimeLeft / _answerTimer.WaitTime * 100;
		}
	}
	private void Add_time_penality()
	{	
		double currentScore = _totalProgressBar.Value;
		double currentCleryScore = _cleryProgressBar.Value;
		double currentArmyScore = _armyProgressBar.Value;
		double currentCommonerScore = _commonerProgressBar.Value;

		_cleryProgressBar.Value = currentCleryScore - 25;
		_armyProgressBar.Value = currentArmyScore - 25;
		_commonerProgressBar.Value = currentCommonerScore - 25;
		_totalProgressBar.Value = currentScore - 25;

        double currentScoreClery = _cleryProgressBar.Value;
        double currentScoreArmy = _armyProgressBar.Value;
        double currentScoreCommoner = _commonerProgressBar.Value;

        if (currentScoreArmy < 50 || currentScoreClery < 50 || currentScoreCommoner < 50)
        {
            GetTree().ChangeSceneToFile(game_over);
        }



    }
    private double CalculateRepPercent()
	{
		return _currentScore / MAX_SCORE;
	}

	public void ClickWord(string word)
	{
		_word = word;
		_sentence.ChangeWord(word);
		_answerTimer.Stop();
		OnAnswerComplete();
	}

	private void OnAnswerComplete()
	{
		//_anim.Play("ZoomOut");
		_wordButtonsManager.SetButtonState(false);
		
		if (_word == "")
		{
			GD.Print("No word given");
			Add_time_penality();
			UpdateTotalScore();
		}
		_delayTimer.Start();
	}

	private void OnDelayComplete()
	{
		_wordButtonsManager.SetButtonState(true);
		//_anim.PlayBackwards("ZoomOut");
		
		ResetRoundState();
		
		_answerTimer.Start();
	}

	private void ResetRoundState()
	{
		CalculateRepPercent();
		_wordButtonsManager.SetWordButtons();
		_word = "";
		currentSentence = _sentence.ChooseSentence();
		_sentence.RestartSentence();
	}
	public void UpdateTotalScore()
	{
		double score = _totalProgressBar.Value;
		_scoreLabel.Text = "Score Total: " + score;
	}
	
	public void UpdateScores(int totalScore, int scoreClergy, int scoreCommoner, int scoreArmy)
	{
		_armyProgressBar.Value += scoreArmy;
		_cleryProgressBar.Value += scoreClergy;
		_commonerProgressBar.Value += scoreCommoner;
		_totalProgressBar.Value = totalScore;
		_scoreLabel.Text = "Score Total: " + totalScore;
		double currentScoreClery = _cleryProgressBar.Value;
		double currentScoreArmy = _armyProgressBar.Value;
		double currentScoreCommoner = _commonerProgressBar.Value;

		if (currentScoreArmy < 50 || currentScoreClery < 50 || currentScoreCommoner < 50)
		{
            GetTree().ChangeSceneToFile(game_over);
        }
		
		if (totalScore >= scoreToWin)
		{
            game_ended = true;
			AfficherMessageGagnant();
		}
	}
	
	private void AfficherMessageGagnant()
	{
		HBoxContainer _wordButtons = GetNode<HBoxContainer>("%WordButtons");
		Sprite2D _podiumZoom = GetNode<Sprite2D>("%PodiumZoom");
		//Label _sentence = GetNode<Label>("%Sentence");
		TextureProgressBar _timerProgress = GetNode<TextureProgressBar>("%TimerProgress");
		
		
		_timerProgress.Visible = false;
		_winningMessage.Visible = true;
		//_countdownLabel.Visible = false;
		//_timerProgress.Visible = false;
		_wordButtons.Visible = false;
		_armyProgressBar.Visible = false;
		_cleryProgressBar.Visible = false;
		_commonerProgressBar.Visible = false;
		_totalProgressBar.Visible = false;
		
		_podiumZoom.Visible = false;
		_sentence.Visible = false; 
		
	}
	
	public void _on_start_pressed()
	{
		GetTree().ChangeSceneToFile(next_level);
	}
}
