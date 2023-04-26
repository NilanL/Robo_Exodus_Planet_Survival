using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretDetection : MonoBehaviour
{
    GameObject gm;
    GameManager gameManager;
    Stats stats;
    Unit_Names unitName;
    Coglings_Attack_AI coglingAI;
    Elf_Attack_AI sleemasiAI;
    Orc_Attack_AI graxxianAI;
    Orc_Attack_AI graxxianArcherAI;
    private float range;

    private float nextActionTime = 0.0f;
    private float period = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        stats = GetComponent<Stats>();
        unitName = GetComponent<Unit_Name>().unit_Name;
        coglingAI = GetComponent<Coglings_Attack_AI>();
        sleemasiAI = GetComponent<Elf_Attack_AI>();
        graxxianArcherAI = this.transform.GetChild(0).gameObject.GetComponent<Orc_Attack_AI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;

            range = stats.getRange();
            switch (unitName)
            {
                case Unit_Names.Cogling_Turret:
                    runCoglingDetection();
                    break;
                case Unit_Names.Sleemasi_Turret:
                    runSleemasiDetection();
                    break;
                case Unit_Names.Graxxian_Turret:
                    runGraxxianDetection();
                    break;
            }
        }
    }

    void runCoglingDetection()
    {
        if (coglingAI.target == null || (coglingAI.target != null && !coglingAI.isAttackingFlag()))
        {
            if (gameManager.selectables != null)
            {
                foreach (var robot in gameManager.selectables)
                {
                    if (robot != null && Vector3.Distance(transform.position, robot.transform.position) < range)
                    {
                        coglingAI.SetTarget(robot);
                        break;
                    }
                }
            }
        }
    }
    
    
    void runSleemasiDetection()
    {
        if (sleemasiAI.target == null || (sleemasiAI.target != null && !sleemasiAI.isAttackingFlag()))
        {
            if (gameManager.selectables != null)
            {
                foreach (var robot in gameManager.selectables)
                {
                    if (robot != null && Vector3.Distance(transform.position, robot.transform.position) < range)
                    {
                        sleemasiAI.SetTarget(robot);
                        break;
                    }
                }
            }
        }
    }

    void runGraxxianDetection()
    {
        if (graxxianArcherAI.target == null || (graxxianArcherAI.target != null && !graxxianArcherAI.isAttackingFlag()))
        {
            if (gameManager.selectables != null)
            {
                foreach (var robot in gameManager.selectables)
                {
                    if (robot != null && Vector3.Distance(transform.position, robot.transform.position) < range)
                    {
                        graxxianArcherAI.SetTarget(robot);
                        break;
                    }
                }
            }
        }
    }
}
