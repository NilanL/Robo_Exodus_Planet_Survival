using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Attack_AI : MonoBehaviour
{
    private GameObject target;
    Stats stats_wolf;
    Orc_Movement_AI coglings_attack;
    bool attacking = false;
    bool canAttack = false;
    bool startAttack = false;


    // Start is called before the first frame update
    void Start()
    {
        stats_wolf = GetComponent<Stats>();
        coglings_attack = GetComponent<Orc_Movement_AI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            if (!target)
            {
                targetDies(null);
            }
            else
                Attacking();
        };
    }

    void Attacking()
    {

        if (Vector3.Distance(target.transform.position, transform.position) < stats_wolf.getRange())
        {
            coglings_attack.InRange();
            if (canAttack || startAttack)
            {
                StartCoroutine(Attack());
            }

        }
        else
        {
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
}
