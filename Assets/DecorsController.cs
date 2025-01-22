 using UnityEngine;

public class DecorsController : MonoBehaviour
{
  [SerializeField] Decor[] decors;
    private void Awake()
    {
        decors = GetComponentsInChildren<Decor>();
        foreach (var item in decors)
        {
         float a=   Random.Range(0, 1.2f);
            item.Init(a);
        }    
    }

}
