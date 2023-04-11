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
    public int Max_Unit_Count = 20;
    FORCombatTest test;
    public List<GameObject> selectables = new List<GameObject>();
    public List<GameObject> buildings = new List<GameObject>();




    //This is just for the test
    private bool spawn = true;
    int count = 5;

    // Start is called before the first frame update
    void Start()
    {
        test = GetComponent<FORCombatTest>();
        selectables = new List<GameObject>(GameObject.FindGameObjectsWithTag("Selectable"));
        buildings = new List<GameObject>(GameObject.FindGameObjectsWithTag("Building"));
        StartCoroutine(UpdateTargetPosition());

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

    public void UnitDied(GameObject unit)
    {
        selectables.Remove(unit);
    }

    IEnumerator UpdateTargetPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            selectables = new List<GameObject>(GameObject.FindGameObjectsWithTag("Selectable"));
            buildings = new List<GameObject>(GameObject.FindGameObjectsWithTag("Building"));
        }
    }


}
