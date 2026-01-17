using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] AudioSource audioSource;
    private void Awake()
    {
        if (instance == null) 
        { 
            instance = this;
        }
        else Destroy(this);
    }
    public void ReproduceAudioClip(AudioClip audioClip, float volume = 1)
    {
        AudioSource aS = Instantiate(audioSource);
        aS.clip = audioClip;
        aS.volume = volume;
        aS.Play();
        Destroy(aS.gameObject,audioClip.length);
    }
}
