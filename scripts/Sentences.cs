using System;
using Godot;
using Godot.Collections;

namespace GamejamV2.scripts;

public partial class Sentences : Node
{
    public static readonly Dictionary<String, Impact> sentences = new()
    {
        {
            "J'offre & aux pauvres.", new Impact(3, 1, 1)
        },
        {
            "Je vole & de l'armée.", new Impact(1, 1, -3)
        },
        {
            "C'est scandaleux d'apprécier &.", new Impact(-2, -2, -2)
        },
        {
            "J'enlève & aux pauvres.", new Impact(-3, 1, 1)
        },
        {
            "Je veux diminuer &.", new Impact(-1, -1, -1)
        },
        {
            "Je veux détruire &.", new Impact(-1, -1, -1)
        },
        {
            "J'augmenterais & dans les régions rurales.", new Impact(2, 1, 1)
        },
        {
            "Mort pour & !", new Impact(-3, -3, -3)
        },
        {
            "Victoire pour & !", new Impact(2, 2, 2)
        },
        {
            "La société devrait investir dans &.", new Impact(1, 1, 1)
        },
        {
            "Au paradis, il y a &.", new Impact(1, 3, 2)
        },
        {
            "C'est toujours & qui cause du malheur.", new Impact(-2, -1, -1)
        },
        {
            "Une de nos meilleurs inventions de notre siècle: &.", new Impact(2, 1, 1)
        },
        {
            "Je vais abattre &.", new Impact(-1, -1, -1)
        },
        {
            "J'aspire à devenir comme &.", new Impact(1, 1, 2)
        },
        {
            "Si j'étais Prince, je bannirais &.", new Impact(-2, -2, -2)
        },
        {
            "Je veux sauver &.", new Impact(2, 2, 2)
        },
        {
            "On devrait encourager &.", new Impact(1, 1, 1)
        },
        {
            "Je voudrais que ça existe, &.", new Impact(2, 1, 1)
        },
        {
            "Je veux obtenir &.", new Impact(1, 1, 1)
        },
        {
            "Je ne crois pas en &.", new Impact(-1, -2, -1)
        },
        {
            "Je voudrais en apprendre plus sur &.", new Impact(1, -1, 1)
        },
        {
            "Je voudrais partager &.", new Impact(1, 1, 2)
        },
        {
            "Je vais conquérir un pays pour &.", new Impact(1, 2, 3)
        },
        {
            "Au nom de l'humanité, je vais éliminer &.", new Impact(-3, -3, -3)
        },
        {
            "Récemment, nous avons découvert &.", new Impact(2, 0, 1)
        },
        {
            "J'aime le clergé car il supporte &.", new Impact(1, 3, 1)
        },
        {
            "Ma philosophie c'est &.", new Impact(2, 2, 2)
        },
        {
            "L'armée se bat pour &.", new Impact(1, 1, 3)
        },
        {
            "Les paysans sont toujours utiles pour &.", new Impact(3, 1, 1)
        },
        {
            "Je vous déteste tous depuis que vous m'avez présenté &.", new Impact(-3, -3, -3)
        },
        {
            "Sans &, le monde serait meilleur.", new Impact(1, 1, 1)
        },
        {
            "Parmi les choses importantes, il y a &", new Impact(1, 1, 1)
        },
        {
            "Si j'étais toi, je choisirais &", new Impact(2, 1, 2)
        },
        {
            "En cas de doute, il faut toujours chercher &", new Impact(1, 1, 3)
        },
    };


    public static int ProcessScore(String sentenceKey, String wordKey, Person[] crowd)
    {
        int score = 0;

        foreach (Person person in crowd)
        {
            switch (person.crowdType)
            {
                case CrowdType.Commoner:
                    score += sentences[sentenceKey].commoner * Words.words[wordKey].commoner;
                    break;

                case CrowdType.Clergy:
                    score += sentences[sentenceKey].clergy * Words.words[wordKey].clergy;
                    break;

                case CrowdType.Army:
                    score += sentences[sentenceKey].army * Words.words[wordKey].army;
                    break;
            }
        }

        int averageScore = crowd.Length > 0 ? score / crowd.Length : 0;
        return averageScore;
    }
}