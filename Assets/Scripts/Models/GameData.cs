using UnityEngine.Events;

public class GameData {
    public UnityEvent OnMatchEnd = new UnityEvent();
    public UnityEvent OnMatchStart = new UnityEvent();
    public string TopTeamName, BotTeamName;
    public bool TopWin, BotWin;

    public TeamPlayMode TopPlayMode { get; internal set; }
    public TeamPlayMode BotPlayMode { get; internal set; }
}