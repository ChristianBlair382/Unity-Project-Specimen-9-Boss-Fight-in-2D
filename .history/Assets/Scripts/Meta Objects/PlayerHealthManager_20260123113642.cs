using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    public Slider healthSlider;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
    
    void Update()
    {
        currentHealth = playerScript.GetHP();
        if(healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }
    }
}
