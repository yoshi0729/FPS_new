using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreenManager : MonoBehaviour
{
    public Text scoreText;
    public Text shootDownText;
    public Text teamAText;
    public Text teamBText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0); // デフォルト値 0
        int shootDownCount = PlayerPrefs.GetInt("ShootDownCount", 0);
        float teamAPercent = PlayerPrefs.GetFloat("TeamAPercent", 0f);
        float teamBPercent = PlayerPrefs.GetFloat("TeamBPercent", 0f);

        scoreText.text = $"スコア: {score}";
        shootDownText.text = $"撃墜数: {shootDownCount}";
        teamAText.text = $"Team A 制圧率: {teamAPercent:F2}%";
        teamBText.text = $"Team B 制圧率: {teamBPercent:F2}%";
    }

    public void OnReturnToTitleButtonClicked()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
