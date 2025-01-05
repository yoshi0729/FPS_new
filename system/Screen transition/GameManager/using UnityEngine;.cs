using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timeText;
    public Text scoreText;

    private int score = 0;
    private int shootDownCount = 0;
    private float timeLeft = 60f;
    private float teamATime = 0f;
    private float teamBTime = 0f;
    private string currentTeam = null; // "A" or "B"

    void Start()
    {
        UpdateUI();
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }

    void UpdateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft--;

            if (currentTeam == "A") teamATime++;
            if (currentTeam == "B") teamBTime++;

            UpdateUI();
        }
        else
        {
            CancelInvoke("UpdateTimer");
            EndGame();
        }
    }

    public void OnShootDownButtonClicked()
    {
        shootDownCount++;
        score += 100;
        UpdateUI();
    }

    public void OnTeamAButtonClicked()
    {
        currentTeam = "A";
    }

    public void OnTeamBButtonClicked()
    {
        currentTeam = "B";
    }

    void EndGame()
    {
        float teamAPercent = (teamATime / 60f) * 100f;
        float teamBPercent = (teamBTime / 60f) * 100f;

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("ShootDownCount", shootDownCount);
        PlayerPrefs.SetFloat("TeamAPercent", teamAPercent);
        PlayerPrefs.SetFloat("TeamBPercent", teamBPercent);
        PlayerPrefs.Save(); // 明示的に保存

        SceneManager.LoadScene("ResultScene");
    }

    void UpdateUI()
    {
        timeText.text = $"残り時間: {timeLeft}s";
        scoreText.text = $"スコア: {score}";
    }
}
