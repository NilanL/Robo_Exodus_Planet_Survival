using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Image healthbarSprite;

    public void UpdateHealthBar(float maxhp, float currenthp)
    {
        healthbarSprite.fillAmount = currenthp / maxhp;
    }
}
