using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth = 100;
    int currentHealth;
    RectTransform healthRectTransform;
    public GameObject explosionPrefab;

    void Start()
    {
        currentHealth = maxHealth;
        healthRectTransform = (RectTransform)gameObject.transform.Find("HealthBar").transform.Find("Health").transform;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(healthRectTransform != null)
            healthRectTransform.localScale = new Vector3(currentHealth / (float)maxHealth, 1f, 1f); ;
    }

    public void Die()
    {
        Debug.Log("Enemy died");
        GameObject explosionGO = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        ParticleSystem explosionParticleSystem = explosionGO.GetComponent<ParticleSystem>();
        float totalDuration = explosionParticleSystem.startLifetime + explosionParticleSystem.duration;
        Destroy(gameObject);
        Destroy(explosionGO, totalDuration);
    }
}
