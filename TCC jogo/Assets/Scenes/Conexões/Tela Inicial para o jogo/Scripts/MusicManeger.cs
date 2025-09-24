using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton para não duplicar
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

    // Método para trocar música
    public void TocarMusica(AudioClip novoClip, bool loop = true)
    {
        if (audioSource.clip == novoClip) return; // evita reiniciar a mesma música
        audioSource.clip = novoClip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    // Método opcional para parar música
    public void PararMusica()
    {
        audioSource.Stop();
    }
}
