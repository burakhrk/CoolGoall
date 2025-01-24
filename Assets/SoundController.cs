using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
  [SerializeField]  AudioClip whistleClip;
    [SerializeField] AudioSource bgSource;

    private void Start()
    {
        bgSource.Play();
    }
    public void PlayWhistle()
    {
        audioSource.PlayOneShot(whistleClip); 
    }
    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
