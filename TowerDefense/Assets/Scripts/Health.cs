using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public int maxHealth = 100;
    int currentHealth;
    public Text healthText;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthText.text = "" + currentHealth;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Die");
    }
}
