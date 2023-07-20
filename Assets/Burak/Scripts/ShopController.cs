using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] PlayerSkin[] buttons;
    [SerializeField] List<PlayerSkin> unlockedSkinList = new List<PlayerSkin>();
    [SerializeField] List<PlayerSkin> skinList = new List<PlayerSkin>();
    CoinController coinController;
    [SerializeField] GameObject formaPanel;
    [SerializeField] GameObject topPanel;

    private void Awake()
    {
        coinController = GetComponent<CoinController>();
        if(PlayerPrefs.GetInt("Unlock0",0)==1)
        {
            unlockedSkinList.Add(buttons[0]);
        }
        if (PlayerPrefs.GetInt("Unlock1", 0) == 1)
        {
            unlockedSkinList.Add(buttons[1]);
        }
        if (PlayerPrefs.GetInt("Unlock2", 0) == 1)
        {
            unlockedSkinList.Add(buttons[2]);
        }
        if (PlayerPrefs.GetInt("Unlock3", 0) == 1)
        {
            unlockedSkinList.Add(buttons[3]);
        }
        if (PlayerPrefs.GetInt("Unlock4", 0) == 1)
        {
            unlockedSkinList.Add(buttons[4]);
        }
        if (PlayerPrefs.GetInt("Unlock5", 0) == 1)
        {
            unlockedSkinList.Add(buttons[5]);
        }
        if (PlayerPrefs.GetInt("Unlock6", 0) == 1)
        {
            unlockedSkinList.Add(buttons[6]);
        }
        if (PlayerPrefs.GetInt("Unlock7", 0) == 1)
        {
            unlockedSkinList.Add(buttons[7]);
        }
        if (PlayerPrefs.GetInt("Unlock8", 0) == 1)
        {
            unlockedSkinList.Add(buttons[8]);
        }

        InitButtons();
        UpdateUnlockedButtons();
    }
    void UpdateUnlockedButtons()
    {
        
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
}
