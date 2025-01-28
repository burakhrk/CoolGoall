using UnityEngine;

public class SoundRandomizer : MonoBehaviour
{
    [SerializeField] AudioClip[] bgClips;
    [SerializeField] AudioSource source;
    private void Awake()
    {
       AudioClip clip= bgClips[Random.Range(0, bgClips.Length)];
      source.clip = clip;
        source.Play();
        
    }
}
