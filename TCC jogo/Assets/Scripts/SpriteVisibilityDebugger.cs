using UnityEngine;

public class SpriteVisibilityDebugger : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
        if (!cam) cam = Camera.main;
        foreach (var e in FindObjectsByType<EnemyStats>(FindObjectsSortMode.None))
        {
            var sr = e.GetComponent<SpriteRenderer>();
            if (!sr) Debug.LogWarning($"{e.name}: sem SpriteRenderer.");
            else
            {
                if (!sr.sprite) Debug.LogWarning($"{e.name}: SpriteRenderer.sprite está vazio.");
                if (!sr.enabled) Debug.LogWarning($"{e.name}: SpriteRenderer desabilitado.");
                if (((1 << e.gameObject.layer) & cam.cullingMask) == 0)
                    Debug.LogWarning($"{e.name}: layer '{LayerMask.LayerToName(e.gameObject.layer)}' fora do Culling Mask da câmera.");
            }
            var vp = cam.WorldToViewportPoint(e.transform.position);
            if (vp.z < cam.nearClipPlane || vp.x < 0 || vp.x > 1 || vp.y < 0 || vp.y > 1)
                Debug.LogWarning($"{e.name}: fora do enquadramento da câmera (viewport {vp}).");
        }
    }
}
