using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacyUpdatePos : MonoBehaviour
{
    Vector3 tempPos;
    GameObject Marker;
    GameObject gm;
    DiplomacyUpdateScript diplomacyUpdate;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        diplomacyUpdate = GameObject.Find("UI").GetComponent<DiplomacyUpdateScript>();
    }

    public void ChangeDiplomacyStatusPositive()
    {
        //Marker = GameObject.Find("Marker");
        //
        //tempPos = Marker.transform.position;
        //
        //tempPos.x += 6f;
        //
        //Marker.transform.position = tempPos;
    }
}
