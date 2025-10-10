using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton para n�o duplicar
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

    // M�todo para trocar m�sica
    public void TocarMusica(AudioClip novoClip, bool loop = true)
    {
        if (audioSource.clip == novoClip) return; // evita reiniciar a mesma m�sica
        audioSource.clip = novoClip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    // M�todo opcional para parar m�sica
    public void PararMusica()
    {
        audioSource.Stop();
    }
}
