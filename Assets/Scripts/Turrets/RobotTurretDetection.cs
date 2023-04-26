using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTurretDetection : MonoBehaviour
{
    GameObject gm;
    GameManager gameManager;
    Stats stats;
    TaskManager taskManager;
    private float range;

    private float nextActionTime = 0.0f;
    private float period = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        stats = GetComponent<Stats>();
        taskManager = GetComponent<TaskManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;

            if (taskManager.target == null || (taskManager.target != null && !taskManager.isAttackingFlag()))
            {
                range = stats.getRange();

                checkForEnemies(gameManager.Coglings);
                checkForEnemies(gameManager.CoglingMiner);
                checkForEnemies(gameManager.Graxian);
                checkForEnemies(gameManager.GraxianMiner);
                checkForEnemies(gameManager.Sleemasi);
                checkForEnemies(gameManager.SleemasiMiner);
            }
        }
    }

    private void checkForEnemies(List<GameObject> enemies)
    {
        if (enemies.Count != 0)
        {
            foreach (var enemy in enemies)
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