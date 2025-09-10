using UnityEngine;

public class DamagePopupSpawner : MonoBehaviour
{
    public GameObject damagePopupPrefab;

    public Transform popupAnchor;

    public void ShowDamage(int damage)
    {
        if (damagePopupPrefab == null || popupAnchor == null)
        {
            Debug.LogError("DamagePopupPrefab ou popupAnchor n�o foi atribu�do!");
            return;
        }

        GameObject popup = Instantiate(damagePopupPrefab, popupAnchor.position, Quaternion.identity);
        popup.GetComponent<DamagePopup>().Setup(damage);
    }



}
