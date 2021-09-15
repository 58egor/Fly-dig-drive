using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGameControl : MonoBehaviour
{
    public float speed = 15f;
    float sp;
    Vector2 startPos;
    Rigidbody rigidbody;
    Vector2 direction;
    bool directionChosen=false;
    // Start is called before the first frame update
    void Start()
    {
        sp = 0;
        rigidbody = gameObject.GetComponent<Rigidbody>();

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
                    startPos = touch.position;
                    // directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    directionChosen = true;
                    Debug.Log("direction:" + direction);
                    //startPos = touch.position;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    sp = 0;
                    directionChosen = false;
                    break;
            }
        }
        if(direction.x > 0.1f && directionChosen)
        {
            sp = speed;
        }
        if (direction.x < -0.1f && directionChosen)
        {
            sp = -speed;
        }
            Vector3 newpos = transform.right * sp * Time.fixedDeltaTime;
            rigidbody.MovePosition(transform.position + newpos);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
}
