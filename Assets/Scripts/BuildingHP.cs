using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHP : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;

    public int currentHealth = 0;
    public int maxHealth = 1000;
    GameObject gm;

    void Start()
    {
        gm = GameObject.Find("GameManager");
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void dmgResource(int damage, GameObject tar)
    {
        currentHealth -= damage;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }
}
