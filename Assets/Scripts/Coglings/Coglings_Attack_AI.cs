using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coglings_Attack_AI : MonoBehaviour
{
    public GameObject target;
    Stats stats_wolf;
    Coglings_Movement_AI coglings_attack;
    bool attacking = false;
    bool canAttack = false;
    bool startAttack = false;
    AnimationController animController;

    // Start is called before the first frame update
    void Start()
    {
        stats_wolf = GetComponent<Stats>();
        coglings_attack = GetComponent<Coglings_Movement_AI>();
        animController = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            if (!target)
            {
                animController.IsNotAttacking();
                animController.IsNotMoving();
                targetDies(null);
            }
            else
                Attacking();
        };
    }

    void Attacking()
    {
        var currRange = stats_wolf.getRange();

        switch (target.GetComponent<Unit_Name>().unit_Name)
        {
            case Unit_Names.Main_Base:
            case Unit_Names.Robot_Turret:
            case Unit_Names.House:
            case Unit_Names.WallGate:
                currRange += 35;
                break;
        }

        if (Vector3.Distance(target.transform.position, transform.position) < currRange)
        {
            if (coglings_attack)
                coglings_attack.InRange();

            if (canAttack || startAttack)
            {
                animController.IsAttacking();
                StartCoroutine(Attack());
            }

        }
        else
        {
            animController.IsNotAttacking();

            if (coglings_attack)
                coglings_attack.OutofRange();
        }

    }

    IEnumerator Attack()
    {
        canAttack = false;
        yield return new WaitForSeconds(stats_wolf.GetAtkSpeed());
        Debug.Log(stats_wolf.GetAtkSpeed(), this.gameObject);
        if (!target)
        {
            targetDies(null);
        }
        if (!(target == null))
        {
            var damage = stats_wolf.GetAtk() - getDefence();
            target.GetComponent<Unit_Health>().dmgResource(damage, this.gameObject);
            canAttack = true;
        }
    }

    public void SetTarget(GameObject tar)
    {
        if (!(target == tar))
        {
            canAttack = true;
        }
        target = tar;
        attacking = true;

    }

    public void targetDies(GameObject tar)
    {
        target = tar;
        attacking = false;
        canAttack = false;
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

    public bool isAttackingFlag()
    {
        return attacking;
    }
}
