using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopCapIncrease : MonoBehaviour
{
    GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
    }
    void UpgradeTo50()
    {
        //gm.GetComponent<GameManager>(). = 50;
    }
    void UpgradeTo75()
    {
        //gm.GetComponent<GameManager>(). = 75;
    }
    void UpgradeTo100()
    {
        //gm.GetComponent<GameManager>(). = 100;
    }
}
