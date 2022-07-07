using System;
using System.Collections;
using UnityEngine;

public class InGameUI : MonoBehaviour {
    private GameData gameData;
    [SerializeField] PlayerInfoUI topInfo, botInfo; 
    [SerializeField] MatchResultBanner matchResultBanner;
    public void SetGameData(GameData gameData)
    {
        if(this.gameData != null)
        {
            this.gameData.OnMatchEnd.RemoveListener(OnMatchEnd);
            this.gameData.OnMatchStart.RemoveListener(OnMatchStart);
        }
        this.gameData = gameData;
        this.gameData.OnMatchEnd.AddListener(OnMatchEnd);
        this.gameData.OnMatchStart.AddListener(OnMatchStart);
    }

    private void OnMatchStart()
    {
        topInfo.PlayerName = gameData.TopTeamName;
        topInfo.PlayerPosition = gameData.TopPlayMode;

        botInfo.PlayerName = gameData.BotTeamName;
        botInfo.PlayerPosition = gameData.BotPlayMode;
    }

    private void OnMatchEnd()
    {
        StartCoroutine(ShowMatchEndResult());
    }

    private IEnumerator ShowMatchEndResult()
    {
        matchResultBanner.BannerText = "DRAW";
        if(gameData.TopWin)
            matchResultBanner.BannerText = $"{gameData.TopTeamName} WIN";
        if(gameData.BotWin)
            matchResultBanner.BannerText = $"{gameData.BotTeamName} WIN";
        matchResultBanner.GetComponent<CanvasAlphaController>().Show();
        yield return new WaitForSeconds(3);
        matchResultBanner.GetComponent<CanvasAlphaController>().Hide();
    }
}