using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Stats : MonoBehaviour
{
    [SerializeField]
    public GameObject Level1WallPrefab = null;

    [SerializeField]
    public GameObject Level2WallPrefab = null;

    [SerializeField]
    public GameObject Level3WallPrefab = null;

    private double level1WallHealth { get; set; } = 100;
    private double level2WallHealth { get; set; } = 200;
    private double level3WallHealth { get; set; } = 300;
    
    public GameObject getLevel1WallObject()
    {
        return Level1WallPrefab;
    }

    public GameObject getLevel2WallObject()
    {
        return Level2WallPrefab;
    }

    public GameObject getLevel3WallObject()
    {
        return Level3WallPrefab;
    }

    public double getLevel1WallHealth()
    {
        return level1WallHealth;
    }

    public double getLevel2WallHealth()
    {
        return level2WallHealth;
    }

    public double getLevel3WallHealth()
    {
        return level3WallHealth;
    }
}
