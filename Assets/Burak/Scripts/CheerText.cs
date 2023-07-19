using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class CheerText : MonoBehaviour
{

   [SerializeField] List<string> cheerList = new List<string>();

    [SerializeField] DOTweenAnimation anim1, anim2;
    private void OnEnable()
    {
        anim1.DOPlay();
        anim2.DOPlay();
        GetComponent<TextMeshProUGUI>().text = cheerList[Random.Range(0,cheerList.Count)];
    } 
}

