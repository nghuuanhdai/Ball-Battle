using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool allowTopSpawn = false;
    [SerializeField] FloatVariable botTeamEnergy, topTeamEnergy;
    [SerializeField] ColorVariable botTeamColor, topTeamColor;
    [SerializeField] FloatVariable spawnConsumption;
    [SerializeField] TouchDetector botFieldDetector, topFieldDetector;
    [SerializeField] EnergyFiller botEnergyFiller, topEnergyFiller;
    [SerializeField] JoyStickController joyStick;
    [SerializeField] BotPlayerController bot;
    [SerializeField] MazeController mazeController;
    [SerializeField] Transform fieldRoot;
    [SerializeField] Goal topGoal, botGoal;
    [SerializeField] Timer timer;

    [SerializeField] Ball ballPrefab;
    [SerializeField] Soldier soldierPrefab;
    
    [SerializeField] IntVariable matchCount;

    private Team currentTopTeam, currentBotTeam;
    private bool enableTouchPlacement;
    private GameResultData gameResult;
    
    public GameResultData GameData => gameResult;

    private void Awake() {
        botFieldDetector.OnTouched.AddListener(SpawnBotSoldier);
        topFieldDetector.OnTouched.AddListener(TopTouchSpawn);
    }

    private void OnDestroy() {
        botFieldDetector.OnTouched.RemoveListener(SpawnBotSoldier);
        topFieldDetector.OnTouched.RemoveListener(TopTouchSpawn);
    }

    private void TopTouchSpawn(Vector3 fieldLocalPosition)
    {
        if(!allowTopSpawn) return;
        SpawnTopSoldier(fieldLocalPosition);
    }

    public void SpawnTopSoldier(Vector3 fieldLocalPosition)
    {
        if(!enableTouchPlacement) return;
        if (topTeamEnergy.Value - spawnConsumption.Value < 0) return;
        var soldier = Instantiate(soldierPrefab, fieldRoot);
        soldier.SetMode(ConvertMode(currentTopTeam.PlayMode));
        soldier.Team = currentTopTeam;
        soldier.transform.localPosition = fieldLocalPosition;
        currentTopTeam.soldiers.Add(soldier);
        soldier.gameObject.SetActive(true);
        topTeamEnergy.Value -= spawnConsumption.Value;
    }

    private void SpawnBotSoldier(Vector3 fieldLocalPosition)
    {
        if(!enableTouchPlacement) return;
        if (botTeamEnergy.Value - spawnConsumption.Value < 0) return;
        var soldier = Instantiate(soldierPrefab, fieldRoot);
        soldier.SetMode(ConvertMode(currentBotTeam.PlayMode));
        soldier.Team = currentBotTeam;
        soldier.transform.localPosition = fieldLocalPosition;
        soldier.gameObject.SetActive(true);
        currentBotTeam.soldiers.Add(soldier);
        botTeamEnergy.Value -= spawnConsumption.Value;
    }

    private SoldierAIMode ConvertMode(TeamPlayMode playMode)
    {
        if(playMode == TeamPlayMode.Attack)
            return SoldierAIMode.Attack;
        if(playMode == TeamPlayMode.Defend)
            return SoldierAIMode.Defend;
        return SoldierAIMode.Manual;
    }

    internal void SetMode(GamePlayMode playMode)
    {
        allowTopSpawn = playMode == GamePlayMode.Multi;
        bot.enabled = playMode == GamePlayMode.Single;
    }

    internal IEnumerator Play(GameData gameData)
    {
        gameResult = new GameResultData();
        CleanField();
        for (int i = 0; i < matchCount.Value; i++)
        {
            CleanField();
            var attackTeam = new Team(TeamPlayMode.Attack);
            var defendTeam = new Team(TeamPlayMode.Defend);

            var ball = Instantiate(ballPrefab, fieldRoot);
            attackTeam.Ball = ball;
            defendTeam.Ball = ball;

            currentTopTeam = i%2==0?attackTeam:defendTeam;
            currentBotTeam = i%2!=0?attackTeam:defendTeam;

            currentTopTeam.Energy = topTeamEnergy;
            currentBotTeam.Energy = botTeamEnergy;

            currentBotTeam.OpponentTeamDirection = topGoal.transform.localPosition - botGoal.transform.localPosition;
            currentTopTeam.OpponentTeamDirection = botGoal.transform.localPosition - topGoal.transform.localPosition;

            currentBotTeam.OpponentGoal = topGoal;
            currentTopTeam.OpponentGoal = botGoal;

            currentBotTeam.AccentColor = botTeamColor;
            currentTopTeam.AccentColor = topTeamColor;

            topGoal.Team = currentTopTeam;
            botGoal.Team = currentBotTeam;

            botEnergyFiller.Reset();
            topEnergyFiller.Reset();

            Team winTeam = null;
            timer.Reset();
            enableTouchPlacement = true;
            gameData.BotPlayMode = currentBotTeam.playMode;
            gameData.TopPlayMode = currentTopTeam.playMode;
            gameData.OnMatchStart.Invoke();
            while (true)
            {
                if(ball.Goal?.Team == defendTeam)
                {
                    winTeam = attackTeam;
                    break;
                }
                if(attackTeam.FailedToPass)
                {
                    winTeam = defendTeam;
                    break;
                }
                if(timer.TimeOut)
                {
                    winTeam = null;
                    break;
                }
                yield return null;
            }
            enableTouchPlacement = false;
            CleanField();

            gameResult.TopPlayerWinMatch.Add(currentTopTeam == winTeam);
            gameResult.BottomPlayerWinMatch.Add(currentBotTeam == winTeam);

            gameData.BotWin = currentBotTeam == winTeam;
            gameData.TopWin = currentTopTeam == winTeam;
            gameData.OnMatchEnd.Invoke();
        }
        
        var topScore = gameResult.TopPlayerWinMatch.FindAll(match => match).Count;
        var botScore = gameResult.BottomPlayerWinMatch.FindAll(match => match).Count;

        if (topScore == botScore)
        {
            CleanField();
            var goalTeam = new Team(TeamPlayMode.Defend);
            var attackTeam = new Team(TeamPlayMode.Attack);

            goalTeam.AccentColor = topTeamColor;
            attackTeam.AccentColor = botTeamColor;

            botGoal.Team = attackTeam;
            topGoal.Team = goalTeam;

            mazeController.Generate();
            yield return mazeController.ShowMaze();

            var soldier = Instantiate(soldierPrefab, fieldRoot);
            soldier.SetMode(SoldierAIMode.Manual);
            soldier.SetJoyStick(joyStick);
            soldier.Team = attackTeam;
            soldier.transform.localPosition = mazeController.EntranceTf.localPosition;
            soldier.gameObject.SetActive(true);
            var ball = Instantiate(ballPrefab, fieldRoot);
            ball.transform.localPosition = mazeController.GetRandomCell().localPosition;
            
            timer.Reset();
            joyStick.gameObject.SetActive(true);
            while (true)
            {
                if(ball.Goal?.Team == goalTeam)
                {
                    gameResult.BottomPlayerWinMatch.Add(true);
                    gameResult.TopPlayerWinMatch.Add(false);
                    break;
                }
                if(timer.TimeOut)
                {
                    gameResult.BottomPlayerWinMatch.Add(false);
                    gameResult.TopPlayerWinMatch.Add(true);
                    break;
                }
                yield return null;
            }

            joyStick.gameObject.SetActive(false);
            mazeController.Clean();
            CleanField();
        }
    }

    private void CleanField()
    {
        for (int i = 0; i < fieldRoot.childCount; i++)
        {
            if(fieldRoot.GetChild(i).gameObject.GetComponent<Goal>()) continue;
            Destroy(fieldRoot.GetChild(i).gameObject);
        }
    }
}