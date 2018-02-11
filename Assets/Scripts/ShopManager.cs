using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {
    public SkinsHolder playerSkinsTemp;
    private List<PlayerSkin> playerSkins;
    public List<bool> skinIsOwned;

    private int currentIndex;
    private int equippedIndex;
    private int coins;
    public Text coinText;

    public MeshRenderer PlayerSkin;
    public MeshRenderer shopSkin;
    public Text buyButtonText;

    public Button LeftButton;
    public Button RightButton;

    public Image coinPurchaseImage;
    

	void Start () {
        playerSkins = playerSkinsTemp.skins;
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinText.text = coins.ToString();
        equippedIndex = -1;
        for(int i = 0; i < playerSkins.Count; i++)
        {
            skinIsOwned.Add(GetBool(playerSkins[i].name, playerSkins[i].isDefault));
            if (PlayerSkin.sharedMaterials[0] == playerSkins[i].materials[0]) equippedIndex = i;
        }
        if(equippedIndex == -1) // No equipped found!
        {
            equippedIndex = 0; // Set to the default Skin
            Equip(equippedIndex);
        }
        currentIndex = equippedIndex;
        UpdateUI();
	}
    public void SetBool(string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
    }
    public bool GetBool(string key, bool defState)
    {
        int value = PlayerPrefs.GetInt(key, defState ? 1 : 0);
        if (value == 1) return true;
        else return false;
    }
    public void UpdateUI()
    {
        if (currentIndex == 0) LeftButton.interactable = false;
        else LeftButton.interactable = true;

        if (currentIndex == playerSkins.Count - 1) RightButton.interactable = false;
        else RightButton.interactable = true;

        updateSkin(shopSkin, playerSkins[currentIndex].materials);
       
        if(currentIndex == equippedIndex)
        {
            buyButtonText.text = "Equipped";
            coinPurchaseImage.enabled = false;
            buyButtonText.color = Color.black;
            return;
        }
        if (skinIsOwned[currentIndex])
        {
            buyButtonText.text = "Equip";
            coinPurchaseImage.enabled = false;
            buyButtonText.color = Color.black;
            return;
        }
        else
        {           
            buyButtonText.text = playerSkins[currentIndex].cost.ToString();
            coinPurchaseImage.enabled = true;
            if (playerSkins[currentIndex].cost > coins) buyButtonText.color = Color.red;
            else buyButtonText.color = Color.black;
            return;
        }
    }
    public static void updateSkin(MeshRenderer mesh, Material[] mats)
    {
        Material[] tempMat = new Material[mesh.sharedMaterials.Length];
        for(int i = 0; i < tempMat.Length; i++)
        {
            Debug.Log("hi");
            if (i >= mats.Length)
            {
                tempMat[i] = mats[mats.Length - 1];
            }
            else
            {
                tempMat[i] = mats[i];
            }
        }
        mesh.sharedMaterials = tempMat;
    }
    public void MoveButton(bool lr)// l = 0 r = 1
    {
        if (lr) currentIndex++;
        else currentIndex--;
        if (currentIndex < 0) currentIndex = 0;
        if (currentIndex >= playerSkins.Count) currentIndex = playerSkins.Count - 1;
        UpdateUI();
    }
    public void Equip(int index)
    {
        PlayerPrefs.SetString("equippedSkin", playerSkins[index].name);
        equippedIndex = index;
        updateSkin(PlayerSkin, playerSkins[index].materials);
        UpdateUI();
    }
    public void PurchaseEquipButton()
    {
        if (currentIndex == equippedIndex) return;
        if (skinIsOwned[currentIndex]) Equip(currentIndex);
        else Purchase(currentIndex);

    }
    public void Purchase(int index)
    {
        if(coins >= playerSkins[index].cost)
        {
            Debug.Log("Purchase!");
            coins -= playerSkins[index].cost;
            PlayerPrefs.SetInt("Coins", coins);
            SetBool(playerSkins[index].name, true);
            skinIsOwned[index] = true;
            coinText.text = coins.ToString();
            Equip(index);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }



}
