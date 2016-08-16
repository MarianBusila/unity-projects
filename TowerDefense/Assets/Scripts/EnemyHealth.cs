using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth = 100;
    int currentHealth;
    RectTransform healthRectTransform;    

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
        //TODO play an explosion
        Destroy(gameObject);
    }
}
