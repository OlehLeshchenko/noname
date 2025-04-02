using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip background, heal, beam, step;

    private void Start ()
    {
        // musicSource.clip = background;
        // musicSource.Play();
    }
    public void HealingSound ()
    {
        sfxSource.PlayOneShot(heal);
    }
     
    public void BeamSound ()
    {
        sfxSource.PlayOneShot(beam);
    }

    public void StepSound () 
    {
        sfxSource.PlayOneShot(step);
    }
}
