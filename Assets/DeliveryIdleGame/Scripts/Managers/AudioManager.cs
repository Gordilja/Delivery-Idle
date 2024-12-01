using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource BgAudio;
    [SerializeField] private AudioSource SfxAudio;

    public void PlaySFX(AudioClip clip) 
    {
        SfxAudio.PlayOneShot(clip);
    }
}