using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IroniteAdd : MonoBehaviour
{
    Text textField;
    GameObject gm;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("Ironite").GetComponent<Text>();
        gm = GameObject.Find("GameManager");
    }

    void Update()
    {
        textField.text = "" + gm.GetComponent<GameManager>().Ironite;
    }

    public void ChangeText()
    {
        num += 1;

        textField.text = "" + num;
    }

}