using UnityEngine;
using DG.Tweening;
public class Decor : MonoBehaviour
{
    Animator animator;

   [SerializeField] DOTweenAnimation Ballanim;
    public void Init(float wait)
    {
        float a = Ballanim.delay;
        Ballanim.delay = wait+a;

animator = GetComponent<Animator>();
        Invoke("StartAnim", wait);
    }
    void StartAnim()
    {
        animator.SetBool("Kick", true);
    }
}
