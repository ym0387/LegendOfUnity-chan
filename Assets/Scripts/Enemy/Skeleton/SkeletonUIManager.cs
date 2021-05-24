using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(SkeletonManager skeletonManager)
    {
        hpSlider.maxValue = skeletonManager.maxHp;
        hpSlider.value = skeletonManager.maxHp;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.value = hp;
    }
}
