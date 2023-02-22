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
    // Start is called before the first frame update
    void Start()
    {
        gamemaster = GameObject.Find("GameManager");
        movement = GetComponent<Robot_Miner_Controller_Mouse>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mining)
        {
            if(!target)
            {
                targetDies(null);
                movement.IsNotMining();
            }
                

            else
                Mining();
        }
    }

    void Mining()
    {
        if(Vector3.Distance(target.transform.position, transform.position) < 5)
        {
            if(canMine)
            {
                StartCoroutine(Mine());
                movement.IsMining();
            }
        }
    }

    IEnumerator Mine()
    {
        canMine = false;
        yield return new WaitForSeconds(timeToMine);
        if (!(target == null))
        {
            target.GetComponent<Resource_HP>().dmgResource(10);
            gamemaster.GetComponent<GameManager>().Ironite += 10;
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
    }

    public void targetDies(GameObject tar)
    {
        target = tar;
        mining = false;
        canMine = false;
    }
}
