using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void TocarMusica(AudioClip novoClip, bool loop = true)
    {
        if (audioSource.clip == novoClip) return;
        audioSource.clip = novoClip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void PararMusica()
    {
        audioSource.Stop();
    }
}
