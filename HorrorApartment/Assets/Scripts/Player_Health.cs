using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Player_Health : MonoBehaviour {

    public int maxHealth = 100;
    private int currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("menu");
    }
}
