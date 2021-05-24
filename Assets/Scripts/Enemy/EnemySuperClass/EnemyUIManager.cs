﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(EnemyManager enemyManager)
    {
        hpSlider.maxValue = enemyManager.maxHp;
        hpSlider.value = enemyManager.maxHp;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.value = hp;
    }
}
