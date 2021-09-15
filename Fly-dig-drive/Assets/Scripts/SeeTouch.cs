using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeTouch : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    image1.SetActive(true);
                    image1.transform.position = touch.position;
                    // directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    image2.SetActive(true);
                    image2.transform.position = touch.position;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    image1.SetActive(false);
                    image2.SetActive(false);
                    break;
            }
        }
    }
}
