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
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
    }
    
    void Update()
    {
        currentStamina = playerScript.GetSTM();
        if(staminaBar.value != currentStamina)
        {
            staminaBar.value = currentStamina;
        }
    }
}
