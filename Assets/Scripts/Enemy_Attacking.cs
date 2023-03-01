using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attacking : MonoBehaviour
{
    private GameObject target;
    Wolf_Stats stats_wolf;
    bool attacking = false;
    bool canAttack = false;
    bool startAttack = false;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        stats_wolf = GetComponent<Wolf_Stats>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attacking)
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
        if (Vector3.Distance(target.transform.position, transform.position) < stats_wolf.GetRange())
        {
            if (canAttack || startAttack)
            {
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsAttacking", true);
                StartCoroutine(Attack());
            }

        }
        else
        {
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsAttacking", false);
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        yield return new WaitForSeconds(stats_wolf.GetAtkSpeed());
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
        if (target.GetComponent<MinerStats>())
        {
            var stats = target.GetComponent<MinerStats>();
            var def = stats.GetDef();
            return def;
        }
        else 
            return 0;
    }
}
