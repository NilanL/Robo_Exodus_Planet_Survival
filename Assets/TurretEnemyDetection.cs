using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyDetection : MonoBehaviour
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
        range = stats.getRange();
        taskManager = GetComponent<TaskManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;

            if (taskManager.target == null && gameManager.enemies != null)
            {
                foreach (var enemy in gameManager.enemies)
                {
                    var dist = Vector3.Distance(transform.position, enemy.transform.position);
                    if (Vector3.Distance(transform.position, enemy.transform.position) < 75)
                    {
                        taskManager.setTarget(enemy);
                        break;
                    }
                }
            }
        }
    }
}
