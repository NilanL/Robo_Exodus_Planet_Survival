using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AurariumAdd : MonoBehaviour
{
    Text textField;
    GameObject gm;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("Aurarium").GetComponent<Text>();
        gm = GameObject.Find("GameManager");

    }

    void Update()
    {
        textField.text = "" + gm.GetComponent<GameManager>().Aurarium;

    }

    public void ChangeText()
    {
        num += 1;

        textField.text = "" + num;
    }

}
