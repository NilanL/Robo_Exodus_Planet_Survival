using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Attack_AI : MonoBehaviour
{
    public GameObject target;
    Stats stats_orc;
    Orc_Movement_AI orcs_attack;
    bool attacking = false;
    bool canAttack = false;
    bool startAttack = false;
    AnimationController animController;

    // Start is called before the first frame update
    void Start()
    {
        stats_orc = GetComponent<Stats>();
        orcs_attack = GetComponent<Orc_Movement_AI>();
        animController = GetComponent<AnimationController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            if (!target)
            {
                if (animController)
                {
                    animController.IsNotAttacking();
                    animController.IsNotMoving();
                }

                targetDies(null);
            }
            else
                Attacking();
        };
    }

    void Attacking()
    {

        if (Vector3.Distance(target.transform.position, transform.position) < stats_orc.getRange())
        {
            if (orcs_attack && orcs_attack.enabled)
                orcs_attack.InRange();

            if (canAttack || startAttack)
            {
                if (animController)
                    animController.IsAttacking();

                StartCoroutine(Attack());
            }

        }
        else
        {
            if (animController)
                animController.IsNotAttacking();

            if (orcs_attack && orcs_attack.enabled)
                orcs_attack.OutofRange();
        }

    }

    IEnumerator Attack()
    {
        canAttack = false;
        yield return new WaitForSeconds(stats_orc.GetAtkSpeed());
        Debug.Log(stats_orc.GetAtkSpeed(), this.gameObject);
        if (!target)
        {
            targetDies(null);
        }
        if (!(target == null))
        {
            var damage = stats_orc.GetAtk() - getDefence();
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

    public GameObject getTarget()
    {
        return target;

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
