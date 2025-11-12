using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Agis_Fala : MonoBehaviour
{
    private bool podeFalar = false;
    private int falaIndex = 0;

    [Header("Referências")]
    public GameObject painelFala;
    public TMP_Text textoFala;
    public Button botaoProximo;
    public GameObject player;
    public SpriteRenderer agisRenderer;

    [Header("Sprites do Agis")]
    public Sprite agisNormal;
    public Sprite agisTransformado;

    [Header("Cena de Batalha Final")]
    public string cenaBatalha = "Final Battle";

    [Header("Efeitos Visuais")]
    public CanvasGroup fadeCanvas;  // objeto preto no topo da tela (alpha controlado)
    public float fadeDuration = 1.5f;
    public CameraShake cameraShake;

    private string[] falasAgis =
    {
        "Você luta por luz… mas não percebe que ela cega.",
        "Eu já fui como você. Ingênuo. Esperançoso. Até ver o que o mundo realmente é.",
        "As pessoas temem a escuridão… mas é nela que a verdade habita.",
        "Eu tentei proteger este mundo uma vez. Eles me chamaram de monstro…",
        "Então que seja! Se é isso que o mundo quer…",
        "Eu serei o monstro que eles temem!",
        "Agora, herói… testemunhe o que resta de um deus quebrado!"
    };

    void Start()
    {
        painelFala.SetActive(false);

        if (botaoProximo != null)
            botaoProximo.onClick.AddListener(ProximaFala);

        // inicia fadeCanvas invisível
        if (fadeCanvas != null)
            fadeCanvas.alpha = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            podeFalar = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            podeFalar = false;
    }

    void Update()
    {
        if (podeFalar && Input.GetKeyDown(KeyCode.Z))
        {
            var playerScript = player.GetComponent<Player>();
            if (playerScript != null)
                playerScript.canMove = false;

            painelFala.SetActive(true);
            falaIndex = 0;
            MostrarFalaAtual();
        }
    }

    void MostrarFalaAtual()
    {
        if (falaIndex < falasAgis.Length)
        {
            textoFala.text = falasAgis[falaIndex];

            // Quando chegar na fala 5 → transformação com shake
            if (falaIndex == 4 && agisRenderer != null && agisTransformado != null)
            {
                StartCoroutine(TransformarAgis());
            }
        }
        else
        {
            StartCoroutine(FinalizarFala());
        }
    }

    IEnumerator TransformarAgis()
    {
        if (cameraShake != null)
            yield return StartCoroutine(cameraShake.Shake(0.5f, 0.3f)); // leve tremor

        agisRenderer.sprite = agisTransformado;
    }

    IEnumerator FinalizarFala()
    {
        // Fade out suave
        if (fadeCanvas != null)
        {
            float tempo = 0;
            while (tempo < fadeDuration)
            {
                fadeCanvas.alpha = Mathf.Lerp(0, 1, tempo / fadeDuration);
                tempo += Time.deltaTime;
                yield return null;
            }
            fadeCanvas.alpha = 1;
        }

        // Reativa movimento e troca cena
        var playerScript = player.GetComponent<Player>();
        if (playerScript != null)
            playerScript.canMove = true;

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(cenaBatalha);
    }

    void ProximaFala()
    {
        falaIndex++;
        MostrarFalaAtual();
    }

    void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

void OnSceneLoaded(Scene cena, LoadSceneMode modo)
{
    
        player = GameObject.Find("Player");
    
}
}
