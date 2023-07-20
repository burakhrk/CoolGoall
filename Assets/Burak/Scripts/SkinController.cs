using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] List<Material> materials;

    [SerializeField] SkinnedMeshRenderer model1;
    [SerializeField] MeshRenderer model2;
    public void ChangeSkin(int a)
    {
        model1.material = materials[a];
        model2.material = materials[a];
    }
}
