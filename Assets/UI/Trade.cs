using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    Text textField;
    Text textField2;
    Text textField3;
    int Zornum;
    int Aurnum;
    int Blonum;
    public GameObject inputField;
    public InputField erase;
    public string customNumber;
    int custnum;
    GameObject gm;
    GameObject Marker;
    Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("Zorium").GetComponent<Text>();
        textField2 = GameObject.Find("Aurarium").GetComponent<Text>();
        textField3 = GameObject.Find("Bloodstone").GetComponent<Text>();
        gm = GameObject.Find("GameManager");
        
    }
    void Update()
    {
        //textField.text = "(Zorium Here) " + gm.GetComponent<GameManager>().Zorium;
        //textField2.text = "(Aurarium Here) " + gm.GetComponent<GameManager>().Aurarium;
        //textField3.text = "(Bloodstone Here) " + gm.GetComponent<GameManager>().BloodStone;
        //textField.text = "(Zorium Here) " + Zornum;
        //textField2.text = "(Aurarium Here) " + Aurnum;
        //textField3.text = "(Bloodstone Here) " + Blonum;
        if (int.TryParse(textField.text, out Zornum))
        {
            
        }
        if (int.TryParse(textField2.text, out Aurnum))
        {

        }
        if (int.TryParse(textField3.text, out Blonum))
        {

        }
    }
    public void ZoriumforAurarium()
    {
        
        if(gm.GetComponent<GameManager>().Aurarium-1 >= 0)
        {
            gm.GetComponent<GameManager>().Zorium += 1;
            gm.GetComponent<GameManager>().Aurarium -= 1;
        }
        else
        {
            return;
        }

        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
    }
    public void ZoriumforAurarium10()
    {
        if (gm.GetComponent<GameManager>().Aurarium - 10 >= 0)
        {
            gm.GetComponent<GameManager>().Zorium += 10;
            gm.GetComponent<GameManager>().Aurarium -= 10;
        }
        else
        {
            return;
        }

        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
    }
    public void ZoriumforAurariumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if(int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().Aurarium - custnum >= 0)
            {
                gm.GetComponent<GameManager>().Zorium = gm.GetComponent<GameManager>().Zorium + custnum;
                gm.GetComponent<GameManager>().Aurarium = gm.GetComponent<GameManager>().Aurarium - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        erase.text = "";
    }
    public void DonateAurariumToCoglings()
    {
        if (gm.GetComponent<GameManager>().Aurarium - 1 >= 0)
        {
            gm.GetComponent<GameManager>().Aurarium -= 1;
        }
        else
        {
            return;
        }
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateAurariumToCoglings10()
    {
        if (gm.GetComponent<GameManager>().Aurarium - 10 >= 0)
        {
            gm.GetComponent<GameManager>().Aurarium -= 10;
        }
        else
        {
            return;
        }
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateAurariumToCoglingsCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            
            if (gm.GetComponent<GameManager>().Aurarium - custnum >= 0)
            {
                gm.GetComponent<GameManager>().Aurarium = gm.GetComponent<GameManager>().Aurarium - custnum;
            }
            else
            {
                return;
            }
        }

        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
    public void ZoriumforBloodstone()
    {
        if (gm.GetComponent<GameManager>().BloodStone - 1 >= 0)
        {
            gm.GetComponent<GameManager>().Zorium += 1;
            gm.GetComponent<GameManager>().BloodStone -= 1;
        }
        else
        {
            return;
        }


        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
    }
    public void ZoriumforBloodstone10()
    {
        if (gm.GetComponent<GameManager>().BloodStone - 10 >= 0)
        {
            gm.GetComponent<GameManager>().Zorium += 10;
            gm.GetComponent<GameManager>().BloodStone -= 10;
        }
        else
        {
            return;
        }

        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
    }
    public void ZoriumforBloodstoneCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().BloodStone - custnum >= 0)
            {
                gm.GetComponent<GameManager>().Zorium = gm.GetComponent<GameManager>().Zorium + custnum;
                gm.GetComponent<GameManager>().BloodStone = gm.GetComponent<GameManager>().BloodStone - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        erase.text = "";
    }
    public void DonateBloodstoneToCoglings()
    {
        if (gm.GetComponent<GameManager>().BloodStone - 1 >= 0)
        {
            gm.GetComponent<GameManager>().BloodStone -= 1;
        }
        else
        {
            return;
        }
        
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateBloodstoneToCoglings10()
    {
        if (gm.GetComponent<GameManager>().BloodStone - 10 >= 0)
        {
            gm.GetComponent<GameManager>().BloodStone -= 10;
        }
        else
        {
            return;
        }
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateBloodstoneToCoglingsCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().BloodStone - custnum >= 0)
            {
                gm.GetComponent<GameManager>().BloodStone = gm.GetComponent<GameManager>().BloodStone - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
    public void AurariumforZorium()
    {
        if (gm.GetComponent<GameManager>().Zorium - 1 >= 0)
        {
            gm.GetComponent<GameManager>().Aurarium += 1;
            gm.GetComponent<GameManager>().Zorium -= 1;
        }
        else
        {
            return;
        }
        
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
    }
    public void AurariumforZorium10()
    {
        if (gm.GetComponent<GameManager>().Zorium - 10 >= 0)
        {
            gm.GetComponent<GameManager>().Aurarium += 10;
            gm.GetComponent<GameManager>().Zorium -= 10;
        }
        else
        {
            return;
        }

        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
    }
    public void AurariumforZoriumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().Zorium - custnum >= 0)
            {
                gm.GetComponent<GameManager>().Aurarium = gm.GetComponent<GameManager>().Aurarium + custnum;
                gm.GetComponent<GameManager>().Zorium = gm.GetComponent<GameManager>().Zorium - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        erase.text = "";
    }
    public void DonateZoriumToSleemasi()
    {
        if (gm.GetComponent<GameManager>().Zorium - 1 >= 0)
        {
            gm.GetComponent<GameManager>().Zorium -= 1;
        }
        else
        {
            return;
        }
        
        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateZoriumToSleemasi10()
    {
        if (gm.GetComponent<GameManager>().Zorium - 10 >= 0)
        {
            gm.GetComponent<GameManager>().Zorium -= 10;
        }
        else
        {
            return;
        }
        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateZoriumToSleemasiCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().Zorium - custnum >= 0)
            {
                gm.GetComponent<GameManager>().Zorium = gm.GetComponent<GameManager>().Zorium - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
    public void AurariumforBloodstone()
    {
        if (gm.GetComponent<GameManager>().BloodStone - 1 >= 0)
        {
            gm.GetComponent<GameManager>().Aurarium += 1;
            gm.GetComponent<GameManager>().BloodStone -= 1;
        }
        else
        {
            return;
        }
        
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
    }
    public void AurariumforBloodstone10()
    {
        if (gm.GetComponent<GameManager>().BloodStone - 10 >= 0)
        {
            gm.GetComponent<GameManager>().Aurarium += 10;
            gm.GetComponent<GameManager>().BloodStone -= 10;
        }
        else
        {
            return;
        }

        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
    }
    public void AurariumforBloodstoneCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().BloodStone - custnum >= 0)
            {
                gm.GetComponent<GameManager>().Aurarium = gm.GetComponent<GameManager>().Aurarium + custnum;
                gm.GetComponent<GameManager>().BloodStone = gm.GetComponent<GameManager>().BloodStone - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        erase.text = "";
    }
    public void DonateBloodstoneToSleemasi()
    {
        if (gm.GetComponent<GameManager>().BloodStone - 1 >= 0)
        {
            gm.GetComponent<GameManager>().BloodStone -= 1;
        }
        else
        {
            return;
        }
        gm.GetComponent<GameManager>().BloodStone -= 1;
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateBloodstoneToSleemasi10()
    {
        if (gm.GetComponent<GameManager>().BloodStone - 10 >= 0)
        {
            gm.GetComponent<GameManager>().BloodStone -= 10;
        }
        else
        {
            return;
        }
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateBloodstoneToSleemasiCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().BloodStone - custnum >= 0)
            {
                gm.GetComponent<GameManager>().BloodStone = gm.GetComponent<GameManager>().BloodStone - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
    public void BloodstoneforZorium()
    {
        if (gm.GetComponent<GameManager>().Zorium - 1 >= 0)
        {
            gm.GetComponent<GameManager>().BloodStone += 1;
            gm.GetComponent<GameManager>().Zorium -= 1;
        }
        else
        {
            return;
        }
        
        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
    }
    public void BloodstoneforZorium10()
    {
        if (gm.GetComponent<GameManager>().Zorium - 10 >= 0)
        {
            gm.GetComponent<GameManager>().BloodStone += 10;
            gm.GetComponent<GameManager>().Zorium -= 10;
        }
        else
        {
            return;
        }

        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
    }
    public void BloodstoneforZoriumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().Zorium - custnum >= 0)
            {
                gm.GetComponent<GameManager>().BloodStone = gm.GetComponent<GameManager>().BloodStone + custnum;
                gm.GetComponent<GameManager>().Zorium = gm.GetComponent<GameManager>().Zorium - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        erase.text = "";
    }
    public void DonateZoriumToGraxxians()
    {
        if (gm.GetComponent<GameManager>().Zorium - 1 >= 0)
        {
            gm.GetComponent<GameManager>().Zorium -= 1;
        }
        else
        {
            return;
        }
        
        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateZoriumToGraxxians10()
    {
        if (gm.GetComponent<GameManager>().Zorium - 10 >= 0)
        {
            gm.GetComponent<GameManager>().Zorium -= 10;
        }
        else
        {
            return;
        }

        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateZoriumToGraxxiansCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().Zorium - custnum >= 0)
            {
                gm.GetComponent<GameManager>().Zorium = gm.GetComponent<GameManager>().Zorium - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField.text = "" + gm.GetComponent<GameManager>().Zorium;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
    public void BloodstoneforAurarium()
    {
        if (gm.GetComponent<GameManager>().Aurarium - 1 >= 0)
        {
            gm.GetComponent<GameManager>().BloodStone += 1;
            gm.GetComponent<GameManager>().Aurarium -= 1;
        }
        else
        {
            return;
        }
        

        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
    }
    public void BloodstoneforAurarium10()
    {
        if (gm.GetComponent<GameManager>().Aurarium - 10 >= 0)
        {
            gm.GetComponent<GameManager>().BloodStone += 10;
            gm.GetComponent<GameManager>().Aurarium -= 10;
        }
        else
        {
            return;
        }

        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
    }
    public void BloodstoneforAurariumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().Aurarium - custnum >= 0)
            {
                gm.GetComponent<GameManager>().BloodStone = gm.GetComponent<GameManager>().BloodStone + custnum;
                gm.GetComponent<GameManager>().Aurarium = gm.GetComponent<GameManager>().Aurarium - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField3.text = "" + gm.GetComponent<GameManager>().BloodStone;
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        erase.text = "";
    }
    public void DonateAurariumToGraxxians()
    {
        if (gm.GetComponent<GameManager>().Aurarium - 1 >= 0)
        {
            gm.GetComponent<GameManager>().Aurarium -= 1;
        }
        else
        {
            return;
        }
        
        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateAurariumToGraxxians10()
    {
        if (gm.GetComponent<GameManager>().Aurarium - 10 >= 0)
        {
            gm.GetComponent<GameManager>().Aurarium -= 10;
        }
        else
        {
            return;
        }

        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateAurariumToGraxxiansCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            if (gm.GetComponent<GameManager>().Aurarium - custnum >= 0)
            {
                gm.GetComponent<GameManager>().Aurarium = gm.GetComponent<GameManager>().Aurarium - custnum;
            }
            else
            {
                return;
            }
            
        }

        textField2.text = "" + gm.GetComponent<GameManager>().Aurarium;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
}