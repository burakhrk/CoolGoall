using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
   
    void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
