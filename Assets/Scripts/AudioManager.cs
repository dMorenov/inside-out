using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource stepsSource;
    
    public AudioClip successClip;

    public AudioClip[] stepsClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySuccess()
    {
        PlayClip(successClip);   
    }
    
    
    public void PlayClip(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }

    public void PlaySteps(int index)
    {
        if (stepsSource.isPlaying) return;
        
        stepsSource.clip = stepsClips[index];
        stepsSource.Play();
    }
    
    public void StopSteps()
    {
        stepsSource.Stop();
    }
}
