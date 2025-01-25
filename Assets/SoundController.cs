using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
  [SerializeField]  AudioClip whistleClip;
    [SerializeField] AudioSource bgSource;
    [SerializeField] AudioSource goalSound;

    public void GoalSound()
    {
        goalSound.Play();
    }
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
