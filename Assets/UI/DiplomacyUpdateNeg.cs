using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacyUpdateNeg : MonoBehaviour
{
    Vector3 tempPos;
    GameObject Marker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeDiplomacyStatusNegative()
    {
        Marker = GameObject.Find("Marker");

        tempPos = Marker.transform.position;

        tempPos.x += -6f;

        Marker.transform.position = tempPos;
    }
}
