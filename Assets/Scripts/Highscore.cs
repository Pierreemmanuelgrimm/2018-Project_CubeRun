using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {
    public Text HighscoreText;
    public Text CoinText;
    public float highscore;
    public int coins;
    private string previousVersion;
    private string key;
    void Start()
    {
        if (Application.isEditor) //Does application is running in editor?
        {
            key = "Editor-Highscore";
            PlayerPrefs.DeleteKey("Highscore");
        }
        else //If not in editor
        {
            key = "Highscore";
        }
       
        // Check version and reset highscore
        previousVersion = PlayerPrefs.GetString("Version", "noVersion");
        if (previousVersion != Application.version)
        {
            PlayerPrefs.SetString("Version", Application.version);
        }
        if(previousVersion == "1.0.0") PlayerPrefs.SetInt("Coins", 0);
        highscore = PlayerPrefs.GetFloat(key, 0);
        UpdateHighScoreText();
        coins = PlayerPrefs.GetInt("Coins", 0);
        UpdateCoinsText();
        
    }
    public void SetNewScore()
    {
        float score = Mathf.Round(PlayerMovement.instance.transform.position.z);
        OnDeathCoinsAdd(score);
        //float score = Mathf.Round(transform.position.z);
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetFloat(key, highscore);
            UpdateHighScoreText();
        }
    }
    void UpdateHighScoreText()
    {
        if (HighscoreText != null) HighscoreText.text = highscore.ToString("0");
    }
    void UpdateCoinsText()
    {
        if (CoinText != null) CoinText.text = coins.ToString();
    }
    public void AddCoins(int n)
    {
        coins += n;
        UpdateCoinsText();
        PlayerPrefs.SetInt("Coins", coins);
    }
    public void OnDeathCoinsAdd(float score)
    {
        if (score < 2000) return;
        int addedCoins = Mathf.FloorToInt(score / 1000);
        AddCoins(addedCoins);
    }
}
