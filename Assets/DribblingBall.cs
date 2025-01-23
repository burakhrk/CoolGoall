using UnityEngine;

public class DribblingBall : MonoBehaviour
{
    public bool hasOwner=false;

    public void BallReceived()
    {
        hasOwner = true;
    }
    public void ShootSent()
    {

    }
}
