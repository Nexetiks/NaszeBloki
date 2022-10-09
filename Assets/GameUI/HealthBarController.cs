using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider slider = null;
    private float maxHP = 100;
    private float hp = 100;

    public void Initialize(float maxHP, float hp)
    {
        this.maxHP = maxHP;
        this.hp = hp;
        slider.maxValue = maxHP;
        slider.minValue = 0;
        slider.value = hp;
    }

    public void getHit(float _demage)
    {
        hp -= _demage;
        slider.value = hp;
    }

}
