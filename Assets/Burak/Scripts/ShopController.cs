using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
     [SerializeField] List<PlayerSkin> unlockedSkinList = new List<PlayerSkin>();
    [SerializeField] List<PlayerSkin> skinList = new List<PlayerSkin>();
    CoinController coinController;
    [SerializeField] GameObject formaPanel;
    [SerializeField] GameObject topPanel;
    SkinController skinController;
    private void Awake()
    {
        coinController = GetComponent<CoinController>();
        if(PlayerPrefs.GetInt("Unlock0",0)==1)
        {
            unlockedSkinList.Add(skinList[0]);
        }
        if (PlayerPrefs.GetInt("Unlock1", 0) == 1)
        {
            unlockedSkinList.Add(skinList[1]);
        }
        if (PlayerPrefs.GetInt("Unlock2", 0) == 1)
        {
            unlockedSkinList.Add(skinList[2]);
        }
        if (PlayerPrefs.GetInt("Unlock3", 0) == 1)
        {
            unlockedSkinList.Add(skinList[3]);
        }
        if (PlayerPrefs.GetInt("Unlock4", 0) == 1)
        {
            unlockedSkinList.Add(skinList[4]);
        }
        if (PlayerPrefs.GetInt("Unlock5", 0) == 1)
        {
            unlockedSkinList.Add(skinList[5]);
        }
        if (PlayerPrefs.GetInt("Unlock6", 0) == 1)
        {
            unlockedSkinList.Add(skinList[6]);
        }
        if (PlayerPrefs.GetInt("Unlock7", 0) == 1)
        {
            unlockedSkinList.Add(skinList[7]);
        }
        if (PlayerPrefs.GetInt("Unlock8", 0) == 1)
        {
            unlockedSkinList.Add(skinList[8]);
        }

        InitButtons();
     }
    private void OnEnable()
    {
        coinController.onCoinChanged += CheckButtonAvailabity;
    }
    private void OnDisable()
    {
        coinController.onCoinChanged -= CheckButtonAvailabity;

    }
    void CheckButtonAvailabity(int a)
    {
        foreach (var item in skinList)
        {
            item.CheckMoney(a);
        }
    }
     public void NewSkinUnlocked(PlayerSkin playerSkin)
    {
        int a = skinList.IndexOf(playerSkin);
        PlayerPrefs.SetInt("Unlock" + a, 1);
    }
    void InitButtons()
    {
        foreach (var item in skinList)
        {
            item.Init(this,coinController,unlockedSkinList.Contains(item));
        }
    }
    public void OpenFormaPanel()
    {
        topPanel.SetActive(false);
        formaPanel.SetActive(true);
    }
    public void OpenTopPanel()
    {

        topPanel.SetActive(true);
        formaPanel.SetActive(false);
    }
    public void SkinSelected(PlayerSkin a)
    {
        skinController.ChangeSkin(skinList.IndexOf(a));
    }
}
