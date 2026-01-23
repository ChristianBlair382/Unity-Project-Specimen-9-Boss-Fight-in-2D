using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    public Slider healthSlider;
    public Slider 
    public float maxHealth = 100f;
    private float currentHealth;
    private bool Initialized = false;

    public void InitializeWithPlayer()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        Initialized = true;
    }
    
    void Update()
    {
        if(!Initialized) return;
        currentHealth = playerScript.GetHP();
        if(healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }
    }
}
