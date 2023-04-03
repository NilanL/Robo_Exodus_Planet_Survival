using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {

    }

    public void IsNotMining()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                var movement = this.gameObject.GetComponent<Robot_Miner_Controller_Mouse>();
                movement.IsNotMining();
                break;
            case Unit_Names.Wolf:
                break;
        }
    }

    public void IsNotAttacking()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                var movement = this.gameObject.GetComponent<Robot_Miner_Controller_Mouse>();
                movement.IsNotAttacking();
                break;
            case Unit_Names.Wolf:
                break;
        }
    }

    public void IsAttacking()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                var movement = this.gameObject.GetComponent<Robot_Miner_Controller_Mouse>();
                movement.IsAttacking();
                break;
            case Unit_Names.Wolf:
                break;
            case Unit_Names.Robot_Melee:
                break;
        }
    }

    public void IsMining()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                var movement = this.gameObject.GetComponent<Robot_Miner_Controller_Mouse>();
                movement.IsAttacking();
                break;
            case Unit_Names.Wolf:
                break;
        }
    }

    public void InRange()
    {
        var unit = this.gameObject.GetComponent<Unit_Name>().unit_Name;
        switch (unit)
        {
            case Unit_Names.Miner:
                break;
            case Unit_Names.Wolf:
                break;
            case Unit_Names.Robot_Ranged:
                var movement = this.gameObject.GetComponent<MovementScript>();
                movement.InRange();
                break;
        }
    }
}
