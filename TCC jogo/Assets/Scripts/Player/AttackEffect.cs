using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public GameObject effectPrefab;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void DestroyAfterAnim()
    {
        Destroy(gameObject);
    }

    public void PlayEffect(Vector3 position)
    {
        GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(effect, 1f);
    }
}
