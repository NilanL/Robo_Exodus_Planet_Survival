using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Health : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;

    public int currentHealth = 0;
    public int maxHealth = 0;
    GameObject gm;
    Unit_Names name;

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

    public void dmgResource(int damage, GameObject tar)
    {
        currentHealth -= damage;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
        switch(GetType())
        {
            case Unit_Names.Miner:
                GetComponent<TaskManager>().gettingAttacked(tar);
                break;
            case Unit_Names.Wolf:
                GetComponent<Wolf_AI>().gettingAttacked(tar);
                break;
        }

    }

    int GetUnitStats()
    {
        int health;
        if(this.gameObject.GetComponent<MinerStats>())
        {
            health = gm.GetComponent<MinerStats>().getMaxHealth();
            return health;
        }
        else if(this.gameObject.GetComponent<Wolf_Stats>())
        {
            health = gm.GetComponent<Wolf_Stats>().getMaxHealth();
            name = this.gameObject.GetComponent<Unit_Name>().unit_Name;
            return health;
        }

        return 10;

    }

    Unit_Names GetType()
    {
        return this.GetComponent<Unit_Name>().unit_Name;

    }

}
