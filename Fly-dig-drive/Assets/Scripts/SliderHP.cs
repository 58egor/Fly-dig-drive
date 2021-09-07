using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderHP : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyInfo enemy;
    Slider slider;
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = enemy.hp;
        slider.value = enemy.hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value > 0)
        {
            slider.value = enemy.hp;
        }
    }
}
