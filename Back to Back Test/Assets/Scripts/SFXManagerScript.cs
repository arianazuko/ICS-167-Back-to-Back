using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManagerScript : MonoBehaviour
{
    public static SFXManagerScript instance { get; private set; }

    public AudioSource m_SFXPlayer;

    public AudioClip[] audioclips;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
    }

    public void PlaySFX(int variable)
    {
        m_SFXPlayer.PlayOneShot(audioclips[variable], Random.Range(0.7f, 1.0f));
    }

    public void PlaySFX(int variable, float volumeLevel)
    {
        m_SFXPlayer.PlayOneShot(audioclips[variable], volumeLevel);
    }

    public void StopAudio()
    {
        m_SFXPlayer.Stop();
    }
}
