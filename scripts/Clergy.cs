using Godot;

namespace GamejamV2.scripts;

public partial class Clergy : Person
{
    [Export] public int SpritesAmmount = 3;
    [Export] public AnimationPlayer clergyAnimate = null!;
    public static int score;

    private Texture2D _bodyNeutral;
    private Texture2D _bodyHappy;
    private Texture2D _bodySad;
    private Texture2D _leftArm;
    private Texture2D _rightArm;
    private TextureRect redAura;
    private TextureRect greenAura;

    public override void _Ready()
    {
        crowdType = CrowdType.Clergy;
        string path = "res://assets/characters/clergy/clergy" + (GD.Randi() % SpritesAmmount + 1);
        string bodyNeutralPath = path + "/body_neutral.png";
        string bodyHappyPath = path + "/body_happy.png";
        string bodySadPath = path + "/body_sad.png";
        string leftArmPath = path + "/left_arm.png";
        string rightArmPath = path + "/right_arm.png";

        _bodyNeutral = (Texture2D)GD.Load(bodyNeutralPath);
        _bodyHappy = (Texture2D)GD.Load(bodyHappyPath);
        _bodySad = (Texture2D)GD.Load(bodySadPath);
        _leftArm = (Texture2D)GD.Load(leftArmPath);
        _rightArm = (Texture2D)GD.Load(rightArmPath);

        GetNode<Sprite2D>("%Body").SetTexture(_bodyNeutral);
        GetNode<Sprite2D>("%LeftArm").SetTexture(_leftArm);
        GetNode<Sprite2D>("%RightArm").SetTexture(_rightArm);

        redAura = GetNode<TextureRect>("%redAura");
        greenAura = GetNode<TextureRect>("%greenAura");
    }


    public override void _Process(double delta)
    {
        string currentAnimation = GetNode<AnimationPlayer>("%AnimationPlayer").CurrentAnimation;
        switch (currentAnimation)
        {
            case "Idle":
                GetNode<Sprite2D>("%Body").SetTexture(_bodyNeutral);
                break;
            case "Happy":
                GetNode<Sprite2D>("%Body").SetTexture(_bodyHappy);
                break;
            case "Sad":
                GetNode<Sprite2D>("%Body").SetTexture(_bodySad);
                break;
        }

        if (score != 0)
        {
            DisplayAura(score > 0);
        } else
        {
            TurnOffAura();
        }
    }

    public void DisplayAura(bool advantageous)
    {
        if(advantageous)
        {
            greenAura.Visible = true;
            redAura.Visible = false;
        } else
        {
            redAura.Visible = true;
            greenAura.Visible = false;
        }
    }

    public void TurnOffAura()
    {
        greenAura.Visible = false;
        redAura.Visible = false;
    }
}