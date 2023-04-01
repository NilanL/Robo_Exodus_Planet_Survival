using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    bool mining = false;
    bool canMine = false;
    Robot_Miner_Controller_Mouse movement;
    GameObject UI;
    public GameObject target;
    GameObject gamemaster;
    public float timeToMine = 2;
    bool attacking = false;
    bool canAttack = false;
    bool startAttack = false;
    MinerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        gamemaster = GameObject.Find("GameManager");
        movement = GetComponent<Robot_Miner_Controller_Mouse>();
        stats = GetComponent<MinerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mining)
        {
            if(!target || !target.GetComponent<ResourceType>())
            {
                targetDies(null);
                movement.IsNotMining();
            }
                

            else
                Mining();
        }

        if (attacking)
        {
            if (!target || !target.GetComponent<Unit_Name>())
            {
                targetDies(null);
                movement.IsNotAttacking();
            }
            else
                Attacking();
        };
    }

    void Attacking()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < 10)
        {

            if (canAttack || startAttack)
            {
                StartCoroutine(Attack());
                movement.IsAttacking();
            }

        }
        else
        {
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        yield return new WaitForSeconds(stats.GetAtkSpeed());
        if (!(target == null))
        {
            var damage = stats.GetAtk() - getDefence();
            target.GetComponent<Unit_Health>().dmgResource(damage, this.gameObject);
            canAttack = true;
        }
    }

    int getDefence()
    {
        if (target.GetComponent<Wolf_Stats>())
        {
            var stats = target.GetComponent<Wolf_Stats>();
            var def = stats.GetDef();
            return def;
        }
        else
            return 0;
    }

    void Mining()
    {
        if(Vector3.Distance(target.transform.position, transform.position) < 10)
        {
            
            if(canMine)
            {
                StartCoroutine(Mine());
                movement.IsMining();
            }
        }
        else
        {
            movement.IsNotMining();
        }
    }

    IEnumerator Mine()
    {
        canMine = false;
        yield return new WaitForSeconds(timeToMine);
        if (!(target == null))
        {
            target.GetComponent<Resource_HP>().dmgResource(10, this.gameObject);
            //gamemaster.GetComponent<GameManager>().Ironite += 10;
            canMine = true;
        }
    }

    public void setTarget(GameObject tar)
    {
        target = tar;
        if(tar.tag == "Resource")
        {
            mining = true;
            canMine = true;
        }
        else if(tar.tag == "Enemy")
        {
            attacking = true;
            canAttack = true;
        }
    }

    public void targetDies(GameObject tar)
    {
        target = tar;
        mining = false;
        canMine = false;
        attacking = false;
        canAttack = false;
    }

    public void gettingAttacked(GameObject tar)
    {
        target = tar;
        attacking = true;
        canAttack = true;
    }
}
