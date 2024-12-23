using UnityEngine;
using UnityEngine.UI;

public class kyoten1 : MonoBehaviour
{
  public float captureTimeToWin = 10f; // 勝利に必要な占領時間
  public Slider progressBar; // プログレスバー
  public Text uiText; // 状況表示用UI
  public string pointName = "Point A"; // エリア名

  private float teamAProgress = 0f;
  private float teamBProgress = 0f;
  private int teamAPlayers = 0; // エリア内のTeam Aプレイヤー数
  private int teamBPlayers = 0; // エリア内のTeam Bプレイヤー数

  private string currentHolder = ""; // 現在の占領中状態
  private bool isCompleted = false; // このエリアが終了したかどうか

  public delegate void OnPointCaptured(string pointName, string winningTeam);
  public event OnPointCaptured PointCaptured; // 勝利通知イベント

  private void Update()
  {
    if (isCompleted) return;

    HandleCaptureProgress();
    UpdateUI();
    CheckWinCondition();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("TeamA"))
    {
      teamAPlayers++;
    }
    else if (other.CompareTag("TeamB"))
    {
      teamBPlayers++;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("TeamA"))
    {
      teamAPlayers = Mathf.Max(0, teamAPlayers - 1);
    }
    else if (other.CompareTag("TeamB"))
    {
      teamBPlayers = Mathf.Max(0, teamBPlayers - 1);
    }
  }

  private void HandleCaptureProgress()
  {
    if (teamAPlayers > teamBPlayers)
    {
      currentHolder = "Team A";
      teamAProgress += Time.deltaTime * (teamAPlayers - teamBPlayers);
      teamBProgress = Mathf.Max(0, teamBProgress - Time.deltaTime * (teamAPlayers - teamBPlayers));
    }
    else if (teamBPlayers > teamAPlayers)
    {
      currentHolder = "Team B";
      teamBProgress += Time.deltaTime * (teamBPlayers - teamAPlayers);
      teamAProgress = Mathf.Max(0, teamAProgress - Time.deltaTime * (teamBPlayers - teamAPlayers));
    }
    else
    {
      currentHolder = "Contested"; // 同数の場合は停止
    }
  }

  private void UpdateUI()
  {
    if (progressBar != null)
    {
      progressBar.value = Mathf.Max(teamAProgress, teamBProgress) / captureTimeToWin;
    }

    if (uiText != null)
    {
      uiText.text = $"{pointName} - Team A: {teamAProgress:F1}s, Team B: {teamBProgress:F1}s\n" +
             $"Currently Holding: {currentHolder}";
    }
  }

  private void CheckWinCondition()
  {
    if (teamAProgress >= captureTimeToWin)
    {
      EndPoint("Team A");
    }
    else if (teamBProgress >= captureTimeToWin)
    {
      EndPoint("Team B");
    }
  }

  private void EndPoint(string winningTeam)
  {
    isCompleted = true;

    if (uiText != null)
    {
      uiText.text = $"{pointName} Captured by {winningTeam}!";
    }

    PointCaptured?.Invoke(pointName, winningTeam); // CapturePointManagerに通知
  }
}