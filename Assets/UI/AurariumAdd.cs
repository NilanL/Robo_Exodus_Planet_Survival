using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AurariumAdd : MonoBehaviour
{
    Text textField;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("Aurarium").GetComponent<Text>();
    }

    public void ChangeText()
    {
        num += 1;

        textField.text = "(Aurarium Here) " + num;
    }

}
