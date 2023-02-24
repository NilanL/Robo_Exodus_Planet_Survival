using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHPIronite : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private IroniteAdd counter;

    public int currentHealth = 0;
    public int maxHealth = 100;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dmgResource(1);
            counter.ChangeText();
            //add to counter
        }
    }

    public void dmgResource(int damage)
    {
        currentHealth -= damage;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }
}
