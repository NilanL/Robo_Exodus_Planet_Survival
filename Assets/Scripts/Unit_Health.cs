using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Health : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;

    public int currentHealth = 0;
    public int maxHealth = 0;
    GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        maxHealth = GetUnitStats();
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

    public void dmgResource(int damage)
    {
        currentHealth -= damage;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }

    int GetUnitStats()
    {
        int health;
        if(this.gameObject.name.ToString().ToLower().Equals("robot_miner_mouse"))
        {
            health = gm.GetComponent<MinerStats>().getMaxHealth();
            return health;
        };

        return 10;

    }

}
