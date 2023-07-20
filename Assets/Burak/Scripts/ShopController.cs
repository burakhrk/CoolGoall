using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] PlayerSkin[] buttons;
    [SerializeField] List<PlayerSkin> unlockedSkinList = new List<PlayerSkin>();
    [SerializeField] List<PlayerSkin> skinList = new List<PlayerSkin>();

    private void Awake()
    {
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

        UpdateUnlockedButtons();
    }
    void UpdateUnlockedButtons()
    {
        foreach (var item in unlockedSkinList)
        {
            item.UnlockedItem();
        }
    }
}
