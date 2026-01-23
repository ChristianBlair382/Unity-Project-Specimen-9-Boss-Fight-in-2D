using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    public Slider healthSlider;
    public Slider easehealthSlider;
    public float 
        maxHealth = 100f,
        lerpSpeed = 1.5f;
    private float currentHealth;
    private bool Initialized = false;

    public void InitializeWithPlayer()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        easehealthSlider.maxValue = maxHealth;
        easehealthSlider.value = currentHealth;
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
        if(healthSlider.value != easehealthSlider.value)
        {
            easehealthSlider.value = Mathf.Lerp(easehealthSlider.value, currentHealth, lerpSpeed);
        }
    }
}
