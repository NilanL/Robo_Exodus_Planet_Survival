using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource_HP : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Healthbar healthbar;

    public int currentHealth = 0;
    public int maxHealth = 100;
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
        if(currentHealth <= 0)
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
