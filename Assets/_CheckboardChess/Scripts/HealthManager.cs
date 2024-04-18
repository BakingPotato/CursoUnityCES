using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    int health;

    [Header("UI")]
    public Slider slider;
    public GameObject sliderColor;
    public Color color;

    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
        sliderColor.GetComponent<Image>().color = color;
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int hp)
    {
        health = hp;
    }
    
    public void takeDamage(int quantity)
    {
        health -= quantity;
        slider.value = health;
    }

    public void gainHealth(int quantity)
    {
        health += quantity;
        slider.value = health;
    }

}
