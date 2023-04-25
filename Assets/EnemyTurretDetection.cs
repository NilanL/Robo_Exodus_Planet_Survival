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
    //Sleemasis_Attack_AI sleemasiAI;
    //Graxxians_Attack_AI graxxianAI;
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

        switch (unitName)
        {
            case Unit_Names.Cogling_Turret:
                coglingAI = GetComponent<Coglings_Attack_AI>();
                break;
            case Unit_Names.Sleemasi_Turret:
                break;
            case Unit_Names.Graxxian_Turret:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;

            switch (unitName)
            {
                case Unit_Names.Cogling_Turret:
                    runCoglingDetection();
                    break;
                case Unit_Names.Sleemasi_Turret:
                    break;
                case Unit_Names.Graxxian_Turret:
                    break;
            }
        }
    }

    void runCoglingDetection()
    {
        if (coglingAI.target == null || (coglingAI.target != null && !coglingAI.isAttackingFlag()))
        {
            if (gameManager.unitsList != null)
            {
                range = stats.getRange();
                foreach (var robot in gameManager.unitsList)
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
    
    /*
    void runSleemasiDetection()
    {
        if (taskManager.target == null || (taskManager.target != null && !taskManager.isAttackingFlag()))
        {
            if (gameManager.enemies != null)
            {
                range = stats.getRange();
                foreach (var enemy in gameManager.enemies)
                {
                    if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) < range)
                    {
                        taskManager.setTarget(enemy);
                        break;
                    }
                }
            }
        }
    }
    void runGraxxianDetection()
    {

    }*/
}
