using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Behavior : MonoBehaviour
{

    bool patrolLeader = false;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<Unit_Name>().unit_Name == Unit_Names.Cogling_Miner)
            patrolLeader = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
