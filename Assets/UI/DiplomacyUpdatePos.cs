using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacyUpdatePos : MonoBehaviour
{
    Vector3 tempPos;
    GameObject Marker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeDiplomacyStatusPositive()
    {
        Marker = GameObject.Find("Marker");

        tempPos = Marker.transform.position;

        tempPos.x += 2f;

        Marker.transform.position = tempPos;
    }
}
