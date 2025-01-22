using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] List<Material> materials;

    [SerializeField] SkinnedMeshRenderer model1;


    [SerializeField] List<GameObject> balls = new List<GameObject>();

   [SerializeField] UIBall iBall;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("ActiveSkin"))
        {
            model1.material = materials[PlayerPrefs.GetInt("ActiveSkin")];
        }
        if (PlayerPrefs.HasKey("ActiveBallSkin"))
        {
            balls[PlayerPrefs.GetInt("ActiveBallSkin")].SetActive(true);
        }
    }
    private void Start()
    {
        iBall.Init();
    }
    public void ChangeBallSkin(int a)
    {
        foreach (var item in balls)
        {
            item.SetActive(false);
        }
        balls[a].SetActive(true);

        PlayerPrefs.SetInt("ActiveBallSkin", a);
    } 
    public void ChangeSkin(int a)
    {
        model1.material = materials[a];
        PlayerPrefs.SetInt("ActiveSkin", a);
        // model2.material = materials[a];
    }
}
