using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip background;
    public AudioClip run;
    public AudioClip swordswing;
    public AudioClip playerhurt;
    public AudioClip enemhurt;
    public AudioClip landing;
    public AudioClip jumping;
    public AudioClip ui;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip); // Play the sound effect
        }
        else
        {
            Debug.LogWarning("SFX source or clip is missing!");
        }
    }

}
