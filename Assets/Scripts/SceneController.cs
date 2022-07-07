using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private InGameUI ingameUI;
    [SerializeField] private GameController gameController;

    private GamePlayMode playMode;
    private GameState gameState;

    private void Awake() {
        mainMenuUI.OnSinglePlayerMode.AddListener(()=> {
            playMode = GamePlayMode.Single;
            gameState = GameState.Playing;
        });
        mainMenuUI.OnMultiplayerMode.AddListener(()=>{
            playMode = GamePlayMode.Multi;
            gameState = GameState.Playing;
        });
        mainMenuUI.OnAR.AddListener(()=>{
            SceneManager.LoadScene("AR");
        });
        gameOverUI.OnReplay.AddListener(()=>{
            gameState = GameState.Playing;
        });
        gameOverUI.OnReturn.AddListener(()=>{
            gameState = GameState.Prepare;
        });
    }

    private IEnumerator Start() {
        HideAllImmediately();
        while (true)
        {
            yield return new WaitUntil(()=>gameState == GameState.Prepare);
            HideAll();
            mainMenuUI.GetComponent<CanvasAlphaController>().Show();

            while (true)
            {
                yield return new WaitUntil(()=>gameState == GameState.Playing);
                gameController.SetMode(playMode);
                HideAll();
                var gameData = new GameData();
                if(playMode == GamePlayMode.Multi)
                {
                    gameData.BotTeamName = "Player 1";
                    gameData.TopTeamName = "Player 2";
                }

                if(playMode == GamePlayMode.Single)
                {
                    gameData.BotTeamName = "Player";
                    gameData.TopTeamName = "Bot";
                }

                ingameUI.SetGameData(gameData);
                ingameUI.GetComponent<CanvasAlphaController>().Show();

                yield return gameController.Play(gameData);
                gameOverUI.ScoreData = gameController.GameData;
                gameState = GameState.GameOver;

                HideAll();
                gameOverUI.GetComponent<CanvasAlphaController>().Show();
                yield return new WaitUntil(()=> gameState != GameState.GameOver);
                
                if(gameState != GameState.Playing)
                    break;
            } 
        }

    }


    private void HideAllImmediately()
    {
        mainMenuUI.GetComponent<CanvasAlphaController>().HideImmediately();
        gameOverUI.GetComponent<CanvasAlphaController>().HideImmediately();
        ingameUI.GetComponent<CanvasAlphaController>().HideImmediately();
    }

    private void HideAll()
    {
        mainMenuUI.GetComponent<CanvasAlphaController>().Hide();
        gameOverUI.GetComponent<CanvasAlphaController>().Hide();
        ingameUI.GetComponent<CanvasAlphaController>().Hide();
    }
}
