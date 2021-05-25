using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(GolemManager golemManager)
    {
        hpSlider.maxValue = golemManager.maxHp;
        hpSlider.value = golemManager.maxHp;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.value = hp;
    }
}
