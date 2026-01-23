using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaManager : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    public Slider staminaBar;
    public float maxStamina = 100f;
    private float currentStamina;
    void Start()
    {
        
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
    }
    
    void Update()
    {
        
    }
}
