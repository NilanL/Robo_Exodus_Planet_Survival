using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Health : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;

    public int currentHealth = 0;
    public int maxHealth = 0;
    GameObject pm;
    GameObject gm;
    Unit_Names name;
    bool start = true;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("ParentCamera");
        gm = GameObject.Find("GameManager");
        maxHealth = GetUnitStats();
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            maxHealth = GetUnitStats();
            currentHealth = maxHealth;
            start = false;
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(Defeated());
        }

    }

    IEnumerator Defeated()
    {
        switch (GetType())
        {
            case Unit_Names.Miner:
                yield return new WaitForSeconds(2);
                break;
            case Unit_Names.Wolf:
                this.gameObject.GetComponent<Animator>().SetBool("IsDefeated", true);
                yield return new WaitForSeconds(2);
                break;
            case Unit_Names.Turret:

            case Unit_Names.Main_Base:
                gm.GetComponent<GameManager>().LoseGame();
                yield return new WaitForSeconds(2);
                break;
        }
        var css = pm.GetComponent<CameraSelectScript>();
        css.Removeselected(this.gameObject);
        if (this.gameObject.tag == "Selectable")
        {
            gm.GetComponent<GameManager>().Unit_count -= 1;
            this.gameObject.GetComponent<FogOfWarDetectionController>().UnitDefeated();
        }
        Destroy(gameObject);

        if (this.gameObject.tag == "Building" || this.gameObject.tag == "EnemyBuilding")
        {

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
            case Unit_Names.Robot_Melee:
                GetComponent<TaskManager>().gettingAttacked(tar);
                break;
        }

    }

    int GetUnitStats()
    {
        int health;
        if(this.gameObject.GetComponent<Stats>())
        {
            health = this.gameObject.GetComponent<Stats>().getMaxHealth();
            return health;
        }

        return 20000;

    }

    Unit_Names GetType()
    {
        return this.GetComponent<Unit_Name>().unit_Name;

    }

}
