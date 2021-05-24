using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Slider hpSlider;
    public Slider staminaSlider;

    public void Init(PlayerManager playerManager)
    {
        //HPの初期値
        hpSlider.maxValue = playerManager.maxHp;
        hpSlider.value = playerManager.maxHp;

        //スタミナの初期値
        staminaSlider.maxValue = playerManager.maxStamina;
        staminaSlider.value = playerManager.maxStamina;
    }

    public void UpdateHP(float hp)
    {
        hpSlider.value = hp;
    }

    public void UpdateStamina(float stamina)
    {
        staminaSlider.value = stamina;
    }
}
