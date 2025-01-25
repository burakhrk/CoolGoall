using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<DribblingBall>() != null)
        {
            FindFirstObjectByType<DribbleGameController>().Lose();
        }
    }
}
