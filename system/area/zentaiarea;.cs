using UnityEngine;

public class CapturePointManager : MonoBehaviour
{
    public CapturePoint[] capturePoints; // 管理する全てのCapturePoint
    public AudioSource audioSource;
    public AudioClip victorySound;
    public AudioClip defeatSound;

    private int teamAWins = 0;
    private int teamBWins = 0;

    public void Start()
    {
        foreach (var point in capturePoints)
        {
            point.PointCaptured += OnPointCaptured; // 各ポイントが終了した際に呼ばれる
        }
    }

    private void OnPointCaptured(string pointName, string winningTeam)
    {
        Debug.Log($"{pointName} was captured by {winningTeam}!");

        if (winningTeam == "Team A")
        {
            teamAWins++;
        }
        else if (winningTeam == "Team B")
        {
            teamBWins++;
        }

        CheckGameEndCondition();
    }

    private void CheckGameEndCondition()
    {
        if (teamAWins == capturePoints.Length)
        {
            EndGame("Team A");
        }
        else if (teamBWins == capturePoints.Length)
        {
            EndGame("Team B");
        }
    }

    private void EndGame(string winningTeam)
    {
        Debug.Log($"{winningTeam} wins the game!");

        if (audioSource != null)
        {
            audioSource.clip = winningTeam == "Team A" ? victorySound : defeatSound;
            audioSource.Play();
        }

        Time.timeScale = 0; // ゲームを停止
    }
}
