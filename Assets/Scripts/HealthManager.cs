using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    public int currentHealth, maxHealth;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Hurt()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.Instance.Respawn();
        }
    }

    public void ResetHealth() => currentHealth = maxHealth;
}
