using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject kale;
    [SerializeField] GameObject top;

    [SerializeField] GameObject topPos;
    [SerializeField] GameObject kalePos;
    [SerializeField] Player player;
    private void Awake()
    {
        topPos.transform.position = top.transform.position;
        kalePos.transform.position = kale.transform.position;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Burak");
        }
    }
    public void Goal()
    {
        player.Goal();
    }
 
}
