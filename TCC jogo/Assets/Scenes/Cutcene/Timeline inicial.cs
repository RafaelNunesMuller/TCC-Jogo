using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System.Collections;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance;

    public PlayableDirector director1;
    public PlayableDirector director2;
    public PlayableDirector director3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistente entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySceneTimeline(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(PlayTimelineNextFrame(sceneName));
    }

    private IEnumerator PlayTimelineNextFrame(string sceneName)
    {
        yield return null; // Espera a cena carregar
        PlayableDirector director = null;

        if (sceneName == "Scene1") director = director1;
        else if (sceneName == "Scene2") director = director2;
        else if (sceneName == "Scene3") director = director3;

        if (director != null) director.Play();
    }

    // Exemplo de sequência automática
    public void PlayAllTimelines()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        PlaySceneTimeline("Scene1");
        yield return new WaitForSeconds((float)director1.duration);
        PlaySceneTimeline("Scene2");
        yield return new WaitForSeconds((float)director2.duration);
        PlaySceneTimeline("Scene3");
        yield return new WaitForSeconds((float)director3.duration);
    }
}
