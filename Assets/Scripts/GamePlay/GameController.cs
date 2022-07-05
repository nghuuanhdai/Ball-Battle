using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private IntVariable matchCount;
    [SerializeField] private FloatVariable matchTime;
    [SerializeField] private FloatVariable matchTimeLeft;
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform fieldRoot;
    [SerializeField] private SoldierPlacement soldierPlacement;

    private IEnumerator Start() {
        var playerTeam = new TeamController();
        var opponentTeam = new TeamController();
        soldierPlacement.PlayerTeam = playerTeam;
        soldierPlacement.OpponentTeam = opponentTeam;

        TeamController[] teamControllers = new TeamController[]{playerTeam, opponentTeam};
        List<MatchResult> playerResults = new List<MatchResult>();
        List<MatchResult> opponentResults = new List<MatchResult>();

        for (int i = 0; i < matchCount.Value; i++)
        {
            var match = new Match(this, teamControllers[i%teamControllers.Length],teamControllers[(i+1)%teamControllers.Length]);
            yield return match.PlayCR();
            playerResults.Add(playerTeam.Result);
            opponentResults.Add(opponentTeam.Result);
            yield return null;
            //show change match overlay
            match.CleanUp();
        }
        
        int playerScore = playerResults.FindAll(rs => rs == MatchResult.Win).Count;
        int opponentScore = opponentResults.FindAll(rs => rs == MatchResult.Win).Count;

        if(playerScore == opponentScore)
        {
            var penatyMatch = new PenatyMatch();
            yield return penatyMatch.PlayCR();
            playerScore += penatyMatch.IsWin?1:0;
            penatyMatch.CleanUp();
        }

        //show game over pannel
    }

    private class PenatyMatch
    {
        private bool isWin;
        public bool IsWin => isWin;

        public IEnumerator PlayCR(){
            yield return null;
        }

        public void CleanUp(){}
    }

    private class Match
    {
        private readonly GameController gameController;
        private readonly TeamController attackTeamController, defendTeamController;
    
        public Match(GameController gameController, TeamController attackTeamController, TeamController defendTeamController)
        {
            this.gameController = gameController;
            this.attackTeamController = attackTeamController;
            this.defendTeamController = defendTeamController;
            this.attackTeamController.PlayMode = PlayMode.Attacker;
            this.defendTeamController.PlayMode = PlayMode.Defender;
        }

        public IEnumerator PlayCR(){
            var ball = Instantiate(gameController.ballPrefab, gameController.fieldRoot);
            gameController.soldierPlacement.Ball = ball;
            while (true)
            {
                yield return null;
                if(ball.IsInGoal)
                {
                    attackTeamController.Result = MatchResult.Win;
                    defendTeamController.Result = MatchResult.Lose;
                    yield break;
                }
                if(attackTeamController.FailedToPass)
                {
                    attackTeamController.Result = MatchResult.Lose;
                    defendTeamController.Result = MatchResult.Win;
                    yield break;
                }
                if(gameController.matchTimeLeft.Value <= 0)
                {
                    attackTeamController.Result = MatchResult.Draw;
                    defendTeamController.Result = MatchResult.Draw;
                    yield break;
                }
            }           
        }

        public void CleanUp(){}
    }
}
