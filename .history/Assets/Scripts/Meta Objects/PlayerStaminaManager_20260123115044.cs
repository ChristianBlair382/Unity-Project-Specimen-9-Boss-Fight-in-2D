using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaManager : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    public Slider staminaSlider;
    public float maxStamina = 100f;
    private float currentStamina;
    private bool Initialized = false;

    void Start()
    {
        // Initialization is now deferred to InitializeWithPlayer()
    }

    public void InitializeWithPlayer()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = currentStamina;
    }
    
    void Update()
    {
        currentStamina = playerScript.GetSTM();
        if(staminaSlider.value != currentStamina)
        {
            staminaSlider.value = currentStamina;
        }
    }
}
