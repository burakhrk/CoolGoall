using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] List<Material> materials;

    [SerializeField] SkinnedMeshRenderer model1;
    [SerializeField] MeshRenderer model2;
    private void Awake()
    {
       if( PlayerPrefs.HasKey("ActiveSkin"))
        {
            model1.material = materials[PlayerPrefs.GetInt("ActiveSkin")];
        }
    }
    public void ChangeSkin(int a)
    {
        model1.material = materials[a];
        PlayerPrefs.SetInt("ActiveSkin", a);
       // model2.material = materials[a];
    }
}
