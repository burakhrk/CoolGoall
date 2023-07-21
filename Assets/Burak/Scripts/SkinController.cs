using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] List<Material> materials;
    [SerializeField] List<Material> Topmaterials;

    [SerializeField] SkinnedMeshRenderer model1;
    [SerializeField] SkinnedMeshRenderer top;

    private void Awake()
    {
       if( PlayerPrefs.HasKey("ActiveSkin"))
        {
            model1.material = materials[PlayerPrefs.GetInt("ActiveSkin")];
        }
    }
    public void ChangeBallSkin(int a)
    {
        model1.material = Topmaterials[a];
        PlayerPrefs.SetInt("ActiveBallSkin", a);
    }
    public void ChangeSkin(int a)
    {
        model1.material = materials[a];
        PlayerPrefs.SetInt("ActiveSkin", a);
       // model2.material = materials[a];
    }
}
