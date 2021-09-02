using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    Rigidbody rigidbody;
    public GameObject[] objects;
    public float speed = 20f;
    float rotSpeed;
    public float rotationSpeed = 10f;
    bool car = true;
    bool jet = false;
    bool drel = false;
    Vector2 startPos;
    Vector2 direction;
    bool directionChosen;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            if (jet)
            {
                jet = false;
                car = true;
                Debug.Log("collision changed");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("collision");
        if (other.gameObject.layer == 10) {
            if (drel)
            {
                Debug.Log("collision changed2");
                drel = false;
                car = true;
            }
        }
    }
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rotSpeed = rotationSpeed;
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    Debug.Log("Все");
                    if (jet)
                    {
                        rotSpeed = -rotationSpeed;
                    }
                    if (drel)
                    {
                        rotSpeed = rotationSpeed;
                    }
                    Debug.Log("Все");
                    break;
            }
        }
        if (direction.y < 0 && !directionChosen)//если палец свайпнули вниз
        {
            Debug.Log("свайп вниз");
            if (car)//и в это время были машиной
            {
                car = false; 
                drel = true;//будм дрелью
                rigidbody.isKinematic = false;
            }
            rotSpeed = -rotationSpeed;
        }
        if(direction.y > 0 && !directionChosen)
        {
            Debug.Log("свайп вверх");
            if (car)//и в это время были машиной
            {
                car = false;
                jet = true;//будм самолетем
                rigidbody.isKinematic = false;
            }
            rotSpeed = rotationSpeed;
        }
        if (car)
        {
            rigidbody.useGravity = true;
            objects[0].SetActive(false);
            objects[1].SetActive(true);
            objects[2].SetActive(false);
        }
        if (jet)
        {
            rigidbody.useGravity = false;
            objects[0].SetActive(true);
            objects[1].SetActive(false);
            objects[2].SetActive(false);
        }
        if (drel)
        {
            rigidbody.useGravity = false;
            objects[0].SetActive(false);
            objects[1].SetActive(false);
            objects[2].SetActive(true);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newpos = transform.right * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(transform.position+newpos);
        float newRot = transform.rotation.eulerAngles.z + rotSpeed * Time.fixedDeltaTime;
        Debug.Log(newRot);
        if (newRot > 270)
        {
            newRot -= 360;
            Debug.Log(newRot);
        }
        if (newRot >= 80)
        {
            Debug.Log("Предел:" + newRot);
            newRot = 80;
        }
        if (newRot <= -80)
        {
            newRot = -80;
        }
        if (!car)
        {
            transform.rotation = Quaternion.Euler(0, 0, newRot);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }
}
