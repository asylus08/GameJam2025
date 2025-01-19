using GamejamV2.scripts;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Crowd : Node2D
{
    public Node2D[] crowdPositions;
    [Export] private AudioStream[] audioHappyList;
    [Export] private AudioStream[] audioSadList;
    [Export] private AudioStream[] audioNeutralList;

    public List<Node2D> armyNodes = new List<Node2D>();
    public List<Node2D> commonerNodes = new List<Node2D>();
    public List<Node2D> clergyNodes = new List<Node2D>();

    private PackedScene commoner;
    private PackedScene clergy;
    private PackedScene soldier;

    public override void _Ready()
    {
        Node crowdPositionsNode = GetNode<Node>("%CrowdPositions");
        crowdPositions = crowdPositionsNode.GetChildren().OfType<Node2D>().ToArray();
        GD.Print(crowdPositions.Length);

        commoner = GD.Load<PackedScene>("res://scenes/Commoner.tscn");
        clergy = GD.Load<PackedScene>("res://scenes/Clergy.tscn");
        soldier = GD.Load<PackedScene>("res://scenes/Soldier.tscn");

        SpawnCrowdMember();
    }


    public override void _Process(double delta)
    {
    }

    public void SpawnCrowdMember()
    {
        if (crowdPositions.Length == 0)
        {
            GD.PrintErr("No crowd positions found.");
            return;
        }

        foreach (Node2D node in crowdPositions)
        {
            int selector = GD.RandRange(0, 2);
            Person crowdMember = null;

            switch (selector)
            {
                case 0:
                    crowdMember = commoner.Instantiate() as Commoner;
                    commonerNodes.Add(crowdMember);
                    break;
                case 1:
                    crowdMember = clergy.Instantiate() as Clergy;
                    clergyNodes.Add(crowdMember);
                    break;
                case 2:
                    crowdMember = soldier.Instantiate() as Soldier;
                    armyNodes.Add(crowdMember);
                    break;
            }

            crowdMember.Position = new Vector2(node.Position.X, node.Position.Y - 250);
            AddChild(crowdMember);
        }
    }

    public void playHappySound()
    {
        if (GD.Randf() < 0.5)
        {
            GetNode<AudioStreamPlayer2D>("%CrowdAudio").Stream =
                audioHappyList[GD.Randi() % (audioHappyList.Length - 1)];
            GetNode<AudioStreamPlayer2D>("%CrowdAudio").Play();
        }
    }

    public void playNeutralSound()
    {
        if (GD.Randf() < 0.5)
        {
            GetNode<AudioStreamPlayer2D>("%CrowdAudio").Stream =
                audioHappyList[GD.Randi() % (audioNeutralList.Length - 1)];
            GetNode<AudioStreamPlayer2D>("%CrowdAudio").Play();
        }
    }

    public void playSadSound()
    {
        if (GD.Randf() < 0.5)
        {
            GetNode<AudioStreamPlayer2D>("%CrowdAudio").Stream = audioSadList[GD.Randi() % (audioHappyList.Length - 1)];
            GetNode<AudioStreamPlayer2D>("%CrowdAudio").Play();
        }
    }

    public void _on_word_buttons_happy_clergy()
    {
        foreach (Clergy clergy in clergyNodes)
        {
            clergy.clergyAnimate.Play("Happy");
            clergy.clergyAnimate.Queue("Idle");
        }

        playHappySound();
    }


    public void _on_word_buttons_happy_army()
    {
        foreach (Soldier soldier in armyNodes)
        {
            soldier.armyAnimate.Play("Happy");
            soldier.armyAnimate.Queue("Idle");
        }

        playHappySound();
    }

    public void _on_word_buttons_happy_commoner()
    {
        foreach (Commoner commoner in commonerNodes)
        {
            commoner.commonerAnimate.Play("Happy");
            commoner.commonerAnimate.Queue("Idle");
        }

        playHappySound();
    }
    
    public void _on_word_buttons_neutral_clergy()
    {
        playNeutralSound();
    }


    public void _on_word_buttons_neutral_army()
    {
        playNeutralSound();
    }

    public void _on_word_buttons_neutral_commoner()
    {
        playNeutralSound();
    }


    public void _on_word_buttons_sad_clergy()
    {
        foreach (Clergy clergy in clergyNodes)
        {
            clergy.clergyAnimate.Play("Sad");
            clergy.clergyAnimate.Queue("Idle");
        }

        playSadSound();
    }


    public void _on_word_buttons_sad_army()
    {
        foreach (Soldier soldier in armyNodes)
        {
            soldier.armyAnimate.Play("Sad");
            soldier.armyAnimate.Queue("Idle");
        }

        playSadSound();
    }

    public void _on_word_buttons_sad_commoner()
    {
        foreach (Commoner commoner in commonerNodes)
        {
            commoner.commonerAnimate.Play("Sad");
            commoner.commonerAnimate.Queue("Idle");
        }

        playSadSound();
    }

}