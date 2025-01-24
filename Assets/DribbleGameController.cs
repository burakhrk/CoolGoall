using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
 public class DribbleGameController : MonoBehaviour
{
    public TextMeshProUGUI goalText;
    bool workOnce = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("BurakBallDragging");
        }
    }
    public void Goal()
    {
        if (workOnce)
            return;

        workOnce = true;
        goalText.gameObject.SetActive(true);
    }
}