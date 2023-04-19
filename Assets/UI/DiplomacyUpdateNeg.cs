using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacyUpdateNeg : MonoBehaviour
{
    Vector3 tempPos;
    GameObject Marker;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    public void ChangeDiplomacyStatusNegative()
    {
        Marker = GameObject.Find("Marker");
        int num = Screen.width;
        tempPos = Marker.transform.position;

        tempPos.x -= (num/99);

        Marker.transform.position = tempPos;
    }
}
