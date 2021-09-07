using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingAnimation : MonoBehaviour
{
    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject canvas;
    bool isActive = true;
    float speed = 80f;
    bool pos = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pos)
        {
            if (transform.position.y-arrow1.transform.position.y > 0.1f)
            {
                Vector3 vec = new Vector3(transform.position.x, arrow1.transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, vec, speed * Time.deltaTime);
            }
            else
            {
                pos = false;
            }
        }
        else
        {
            if (arrow2.transform.position.y-transform.position.y > 0.1f)
            {
                Vector3 vec = new Vector3(transform.position.x, arrow2.transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, vec, speed * Time.deltaTime);
            }
            else
            {
                pos = true;
            }
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    isActive = false;
                    break;
            }

        }
        if (!isActive)
        {
            canvas.SetActive(false);
        }
    }
}
