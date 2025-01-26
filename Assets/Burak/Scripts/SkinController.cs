using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] List<Material> materials;
 
    [SerializeField] SkinnedMeshRenderer model1;


    [SerializeField] List<GameObject> balls = new List<GameObject>(); 

    [SerializeField] UIPlayer iPlayer;

    private void Awake()
    {
       if( PlayerPrefs.HasKey("ActiveSkin"))
        {
            model1.material = materials[PlayerPrefs.GetInt("ActiveSkin",1)]; 
        }
        if (PlayerPrefs.HasKey("ActiveBallSkin"))
        {
            balls[PlayerPrefs.GetInt("ActiveBallSkin")].SetActive(true);
        }
    } 
    public void ChangeBallSkin(int a)
    {
        foreach (var item in balls)
        {
            item.SetActive(false);
        }
        balls[a].SetActive(true);
       
        PlayerPrefs.SetInt("ActiveBallSkin", a);


        iPlayer.ChangeBallSkin(a);

    }
    public void ChangeSkin(int a)
    { 
        model1.material = materials[a];
        PlayerPrefs.SetInt("ActiveSkin", a);
        iPlayer.ChangeSkin(a);
       // model2.material = materials[a];
    }
}
