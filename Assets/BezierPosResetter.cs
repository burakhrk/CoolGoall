using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class BezierPosResetter : MonoBehaviour
{
    public float yLimit = 10f;
    void LateUpdate()
    {
      if  (Input.GetMouseButtonUp(0))
        {
            transform.position = Vector3.zero;
        }
      if(transform.position.y>=yLimit)
        {
            transform.position = new Vector3(transform.position.x, yLimit, transform.position.z);
        }
    }
}
