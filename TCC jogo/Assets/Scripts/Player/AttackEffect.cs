using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public GameObject effectPrefab; // arraste sua animação aqui

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // chamado no final da animação (adicione um Animation Event)
    public void DestroyAfterAnim()
    {
        Destroy(gameObject);
    }

    public void PlayEffect(Vector3 position)
    {
        GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(effect, 1f); // destrói após 1s (tempo da animação)
    }
}
