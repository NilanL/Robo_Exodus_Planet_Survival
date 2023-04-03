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

    private bool spawn = true;

    // Start is called before the first frame update
    void Start()
    {
        
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
        yield return new WaitForSeconds(600);
        Debug.Log("Spawn Now");
        spawn = true;
    }

    public void GetMinerStats()
    {

    }
}
