using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoriumAdd : MonoBehaviour
{
    Text textField;
    int num;
    GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("Zorium").GetComponent<Text>();
        gm = GameObject.Find("GameManager");

    }
    void Update()
    {
        textField.text = "(Zorium Here) " + gm.GetComponent<GameManager>().Zorium;

    }
    public void ChangeText()
    {
        num += 1;

        textField.text = "(Zorium Here) " + num;
    }

}
