using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int Ironite;
    public int BloodStone;
    public int Aurarium;
    public int Zorium;
    public int Unit_count;
    FORCombatTest test;

    //This is just for the test
    private bool spawn = true;
    int count = 5;

    // Start is called before the first frame update
    void Start()
    {
        test = GetComponent<FORCombatTest>();

    }

    // Update is called once per frame
    void Update()
    {

        //This is just for the Test for getting balance for the first enemies
        if(spawn)
            StartCoroutine(Spawn());


    }

    IEnumerator Spawn()
    {
        spawn = false;
        yield return new WaitForSeconds(120);
        test.ChooseUnits(count);
        count += 5;
        test.SetTarget();
        spawn = true;
    }

    public void GetMinerStats()
    {

    }
}
