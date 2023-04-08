using System.Collections;
using System.Collections.Generic;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class ScoreEntry
{
    Label Rank, Tier, Name, Score;
    public VisualElement Panel;
   
    public void SetVisualElements(VisualElement visuals)
    {
        Panel = visuals.Q<VisualElement>("ScoreEntry");
        Rank = visuals.Q<Label>("Rank");
        Tier = visuals.Q<Label>("Tier");
        Name = visuals.Q<Label>("Name");
        Score = visuals.Q<Label>("Score");
    }

    public void SetData(LeaderboardEntry entry)
    {
        Rank.text = entry.Rank.ToString();
        Tier.text =  entry.Tier;
        Name.text =  entry.PlayerName;
        Score.text = entry.Score.ToString();
    }
        
}