using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {
    public MeshRenderer playerGFX;
    public SkinsHolder playerSkins;
    public void Start()
    {
        EquipSavedSkin();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void StartInfinite()
    {
        SceneManager.LoadScene("Infinite");
    }
    public void OpenShop()
    {
        
        SceneManager.LoadScene("Shop");
    }
    public void EquipSavedSkin()
    {
        for(int i = 0; i < playerSkins.skins.Count; i++)
        {
            if (PlayerPrefs.GetString("equippedSkin", "none") == playerSkins.skins[i].name) ShopManager.updateSkin(playerGFX, playerSkins.skins[i].materials);
        }
    }

}
