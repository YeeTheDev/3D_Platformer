using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    public int currentHealth, maxHealth;

    public float invincibleLength = 2f;
    private float invinCounter;

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
        if (invinCounter > 0)
        {
            invinCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.Instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(invinCounter * 5f) % 2 == 0)
                {
                    PlayerController.Instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.Instance.playerPieces[i].SetActive(false);
                }

                if (invinCounter <= 0)
                {
                    PlayerController.Instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }

    public void Hurt()
    {
        if (invinCounter <= 0)
        {

            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.Instance.Respawn();
            }
            else
            {
                PlayerController.Instance.KnockBack();
                invinCounter = invincibleLength;

                for (int i = 0; i < PlayerController.Instance.playerPieces.Length; i++)
                {
                    PlayerController.Instance.playerPieces[i].SetActive(false);
                }
            }
        }
    }

    public void ResetHealth() => currentHealth = maxHealth;

    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
