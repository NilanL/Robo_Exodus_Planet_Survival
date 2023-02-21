using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodstoneAdd : MonoBehaviour
{
    Text textField;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("Bloodstone").GetComponent<Text>();
    }

    public void ChangeText()
    {
        num += 1;

        textField.text = "(Bloodstone Here) " + num;
    }

}