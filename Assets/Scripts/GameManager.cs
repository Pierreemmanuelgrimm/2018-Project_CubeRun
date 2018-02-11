using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    bool gameHasEnded = false;
    public Highscore highscore;
    public GameObject playerPrefab;

    public float restartDelay = 1f;

    public GameObject completeLevelUI;

    public void Awake()
    {
        instance = this;

        GameObject player = Instantiate(playerPrefab);
        player.transform.position = new Vector3(0, 1, 0);
        
    }
    public void CompleteLevel()
    {
        Debug.Log("End");
        completeLevelUI.SetActive(true);
    }
    public void EndGame ()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            if (highscore != null) highscore.SetNewScore();
            Debug.Log("GAME OVER");
            Invoke("Restart",restartDelay);
        }
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
