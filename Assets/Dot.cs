using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    public void Disable()
    {
        meshRenderer.enabled = false;
    }
}
