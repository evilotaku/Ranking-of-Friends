using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Leaderboard : MonoBehaviour
{
    public string LeaderboardId;
    public VisualTreeAsset ScoreEntryTemplate;
    UIDocument LeaderboardUI;
    ListView _leaderboard;
    List<LeaderboardEntry> _scoreList;

    async void Awake()
    {
        _scoreList = new();
        LeaderboardUI = GetComponent<UIDocument>();
        _leaderboard = LeaderboardUI.rootVisualElement.Q<ListView>("leaderboard");        
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
            print("Player Name is " + AuthenticationService.Instance.PlayerName);
        };
        AuthenticationService.Instance.SignInFailed += s =>
        {
            // Take some action here...
            Debug.Log(s);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        await CreateLeaderboardAsync();
    }

    async Task CreateLeaderboardAsync()
    {
        var request = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        _scoreList = request.Results;
        _leaderboard.makeItem = () =>
        {
            var newScoreEntry = ScoreEntryTemplate.Instantiate();
            var newScoreLogic = new ScoreEntry();
            newScoreEntry.userData = newScoreLogic;
            newScoreLogic.SetVisualElements(newScoreEntry);
            return newScoreEntry;
        };

        _leaderboard.bindItem = (item, index) =>
        {
            (item.userData as ScoreEntry).SetData(_scoreList[index]);
        };

        //_leaderboard.fixedItemHeight = 50;
        _leaderboard.itemsSource = _scoreList;
        _leaderboard.RefreshItems();
    }

    [ContextMenu("Update Leaderboard")]
    public async Task UpdateLeaderBoardAsync()
    {
        _leaderboard.Clear();
        print("Updating Leaderboard...");
        var request = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        _scoreList = request.Results;
        print("Scores Updated...");
        _leaderboard.RefreshItems();
        
       
    }
}
