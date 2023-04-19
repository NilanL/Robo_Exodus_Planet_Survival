using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacyUpdatePos : MonoBehaviour
{
    Vector3 tempPos;
    GameObject Marker;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    public void start()
    {
        Marker = GameObject.Find("Marker");

        tempPos = Marker.transform.position;

        tempPos.x += gm.reputation;

        Marker.transform.position = tempPos;
    }

    public void ChangeDiplomacyStatusPositive()
    {
        Marker = GameObject.Find("Marker");

        tempPos = Marker.transform.position;
        gm.reputation = (int)tempPos.x;
        tempPos.x += 6f;

        Marker.transform.position = tempPos;
    }
}
