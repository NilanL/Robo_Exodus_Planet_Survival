using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public bool mining = false;
    bool canMine = false;
    AnimationController movement;
    GameObject UI;
    public GameObject target;
    GameObject gamemaster;
    public float timeToMine = 2;
    bool attacking = false;
    bool canAttack = true;
    bool startAttack = false;
    Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        gamemaster = GameObject.Find("GameManager");
        movement = GetComponent<AnimationController>();
        stats = GetComponent<Stats>();
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
                checkforotherUnits();
                movement.IsNotAttacking();
            }
            else
                Attacking();
        };
    }

    void checkforotherUnits()
    {
        var tars = GameObject.FindGameObjectsWithTag("Cogling");
        foreach (var tar in tars)
        {
            if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 40)
            {
                target = tar;
            }
        }
        tars = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var tar in tars)
        {
            if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 40)
            {
                target = tar;
            }
        }
    }


    void Attacking()
    {
        int currRange = stats.getRange();

        switch (target.GetComponent<Unit_Name>().unit_Name)
        {
            case Unit_Names.Main_Base:
            case Unit_Names.Cogling_Turret:
            case Unit_Names.Sleemasi_Turret:
            case Unit_Names.Graxxian_Turret:
            case Unit_Names.House:
            case Unit_Names.WallGate:
                currRange += 35;
                break;
        }

        if (Vector3.Distance(target.transform.position, transform.position) < currRange)
        {
            if (canAttack)
            {
                StartCoroutine(Attack());
                movement.IsAttacking();
            }

            movement.InRange();
        }
        else
        {
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        float atkspd = stats.GetAtkSpeed();
        yield return new WaitForSeconds(atkspd);
        if (!(target == null))
        {
            var damage = stats.GetAtk() - getDefence();
            //Debug.Log(damage);
            target.GetComponent<Unit_Health>().dmgResource(damage, this.gameObject);
            canAttack = true;
        }
    }

    int getDefence()
    {
        if (target.GetComponent<Stats>())
        {
            var stats = target.GetComponent<Stats>();
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
            
        }
        canMine = true;
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

            if (target != tar)
            {
                canAttack = true;
            }
        }
        else if(tar.tag == "Cogling")
        {
            attacking = true;

            if (target != tar)
            {
                canAttack = true;
            }
        }
        else if (tar.tag == "Graxian")
        {
            attacking = true;

            if (target != tar)
            {
                canAttack = true;
            }
        }
        else if (tar.tag == "Sleemasi")
        {
            attacking = true;

            if (target != tar)
            {
                canAttack = true;
            }
        }
        else if (tar.tag == "Graxian")
        {
            attacking = true;

        }
        else if (tar.tag == "Sleemasi")
        {
            attacking = true;

        }
    }

    public void targetDies(GameObject tar)
    {
        target = tar;
        mining = false;
        canMine = false;
        attacking = false;
    }

    public void gettingAttacked(GameObject tar)
    {
        target = tar;
        attacking = true;
    }

    public bool isAttackingFlag()
    {
        return attacking;
    }
}
