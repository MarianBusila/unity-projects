using UnityEngine;
using System.Collections;

public class Enemy_Health : MonoBehaviour {

    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
	}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("isHit");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Kill the Enemy
        Destroy(gameObject);
    }
	
}
