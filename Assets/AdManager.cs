using UnityEngine;
using CrazyGames;
using UnityEngine.SceneManagement;
public class AdManager : MonoBehaviour
{
    private void Awake()
    {
         if (CrazySDK.IsAvailable)
        {
            CrazySDK.Init(() =>
            {
                Debug.Log("CrazySDK initialized");
             });
        }
    }
}
