using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    public void ReturnMenu()
    {
        Debug.Log("MENU");
        SceneManager.LoadScene(0);
    }
}
