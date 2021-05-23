using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(SlimeManager slimeManager)
    {
        hpSlider.maxValue = slimeManager.maxHp;
        hpSlider.value = slimeManager.maxHp;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.value = hp;
    }
}
