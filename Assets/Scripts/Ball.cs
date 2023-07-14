using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Ball : MonoBehaviour
{
    Vector3[] path;
    [SerializeField] float speed = 0.02f;
    bool collision = false;
     public void Shoot(Vector3[] vector3)
    {
        path = vector3;
        Invoke("ShootWaited", 1f);
    }
    void ShootWaited()
    {
        StartCoroutine(MovePath());
    }
    IEnumerator MovePath()
    {
        for (int i = 0; i < path.Length-1; i++)
        {
            if (collision)
                yield break;

            transform.DOMove(path[i], speed).SetEase(Ease.Linear);
            yield return new WaitForSeconds(speed);
        }
       
    }
    void Collision()
    {
        //StopCoroutine(MovePath());
        collision = true;

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Collision();
            Enemy.isDefence = true;
        }
          

        Debug.Log(other.transform.name);
    }
}
