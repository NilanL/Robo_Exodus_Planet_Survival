using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTroopTesting : MonoBehaviour
{
    Text textField;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("Troops").GetComponent<Text>();
    }

    public void ChangeText()
    {
        num += 1;
        if (num > 100)
        {
            num -= 1;
        }
            
        textField.text = "Troops: " + num + "/100";
    }


}