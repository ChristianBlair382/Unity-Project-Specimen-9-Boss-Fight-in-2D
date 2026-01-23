using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaManager : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    public Slider staminaSlider;
    public Slider easestaminaSlider;
    public float 
        maxStamina = 100f,
        lerpSpeed = 10f;
    private float currentStamina;
    private bool Initialized = false;

    public void InitializeWithPlayer()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = currentStamina;
        easestaminaSlider.maxValue = maxStamina;
        easestaminaSlider.value = currentStamina;
        Initialized = true;
    }
    
    void Update()
    {
        if(!Initialized) return;
        currentStamina = playerScript.GetSTM();
        if(staminaSlider.value != currentStamina)
        {
            staminaSlider.value = currentStamina;
        }
        if(staminaSlider.value != easestaminaSlider.value)
        {
            easestaminaSlider.value = Mathf.Lerp(easestaminaSlider.value, currentStamina, lerpSpeed);
        }
    }
}
