using Godot;
using System;
using System.Collections.Generic;

namespace GamejamV2.scripts;
public partial class GameSentence : Label
{
    private string _selectedWord = "__________";
    private string _selectedSentence;

    public override void _Ready()
    {
        _selectedSentence = ChooseSentence();
        ChangeWord(_selectedWord);
    }
	
    public void ChangeWord(string word)
    {
        _selectedWord = word;
        Text = _selectedSentence.Replace("&", word);
    }

    public String ChooseSentence()
    {
        Random rand = new Random();
        List<String> sentenceKeys = new List<String>(Sentences.sentences.Keys);
        String selectedKey = sentenceKeys[rand.Next(0, sentenceKeys.Count - 1)];
        _selectedSentence = selectedKey;
        return selectedKey;
    }

    public void RestartSentence()
    {
        _selectedWord = "__________";
        ChangeWord(_selectedWord);
    }
}