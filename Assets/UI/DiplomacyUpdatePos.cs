using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacyUpdatePos : MonoBehaviour
{
    Vector3 tempPos;
    GameObject Marker;
    GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeDiplomacyStatusPositiveCogling()
    {
        Marker = GameObject.Find("Marker");
        gm = GameObject.Find("GameManager");

        gm.GetComponent<GameManager>().CoglingRep += 1;

        tempPos = Marker.transform.position;

        tempPos.x += 6f;

        Marker.transform.position = tempPos;
    }
    public void ChangeDiplomacyStatusPositiveSleemasi()
    {
        Marker = GameObject.Find("Marker");
        gm = GameObject.Find("GameManager");

        gm.GetComponent<GameManager>().SleemasiRep += 1;

        tempPos = Marker.transform.position;

        tempPos.x += 6f;

        Marker.transform.position = tempPos;
    }
    public void ChangeDiplomacyStatusPositiveGraxxian()
    {
        Marker = GameObject.Find("Marker");
        gm = GameObject.Find("GameManager");

        gm.GetComponent<GameManager>().GraxxianRep += 1;

        tempPos = Marker.transform.position;

        tempPos.x += 6f;

        Marker.transform.position = tempPos;
    }
}
