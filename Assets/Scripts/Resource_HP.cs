using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource_HP : MonoBehaviour
{
    // Start is called before the first frame update
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
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void dmgResource(int damage, GameObject tar)
    {
        currentHealth -= damage;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
        GetType_addResource(damage);
    }

    private void GetType_addResource(int damage)
    {
        var type = this.gameObject.GetComponent<ResourceType>().resource;
        switch(type)
        {
            case ResourceTypes.Ironite:
                gm.GetComponent<GameManager>().Ironite += damage;
                break;
            case ResourceTypes.Bloodstone:
                gm.GetComponent<GameManager>().BloodStone += damage;
                break;
            case ResourceTypes.Aurarium:
                gm.GetComponent<GameManager>().Aurarium += damage;
                break;
            case ResourceTypes.Zorium:
                gm.GetComponent<GameManager>().Zorium += damage;
                break;
        }
    }
}
