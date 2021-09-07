using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    Slider slider;
    Vector3 finish;
    // Start is called before the first frame update
    void Start()
    {
        finish = end.transform.position;
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = Vector3.Distance(finish, start.transform.position);
        Debug.Log("max:" + slider.maxValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < slider.maxValue && start!=null)
        {
            slider.value = slider.maxValue - Vector3.Distance(finish, start.transform.position);
        }
    }
}
