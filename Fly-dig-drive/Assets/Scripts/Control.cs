using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    Rigidbody rigidbody;
    public GameObject loose;
    public TrailRenderer[] dirts;
    public GameObject[] objects;
    public float speed = 20f;
    float rotSpeed;
    public float maxDegree = 70;
    public float rotationSpeed = 10f;
    bool car = true;
    bool jet = false;
    bool drel = false;
    bool firstTime = false;
    Vector2 startPos;
    Vector2 direction;
    bool directionChosen;
    float stun = 0.5f;
    float stunActive=0;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            if (jet)
            {
                jet = false;
                car = true;
                drel = false;
                Debug.Log("collision changed");
                stunActive = stun;
                if (direction.y > 0.1f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

        Debug.Log("Stay2");
        if (other.gameObject.layer == 10)
        {
            if (drel && transform.position.y>0.1f)
            {
                Debug.Log("collision changed2");
                drel = false;
                car = true;
                jet = false;
                stunActive = stun;
                if(direction.y < -0.1f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
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
                    // directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    directionChosen = false;
                    Debug.Log("direction:" + direction);
                    //startPos = touch.position;
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
        if (direction.y < -0.1f && !directionChosen)//если палец свайпнули вниз
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
        if(direction.y > 0.1f && !directionChosen)
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
        Debug.Log("Rot:" + rotSpeed);
        float newRot = transform.rotation.eulerAngles.z + rotSpeed * Time.fixedDeltaTime;
        Debug.Log(newRot);
        if (newRot >= 360 - maxDegree-10)
        {
            newRot -= 360;
            Debug.Log("new:"+newRot);
        }
        Debug.Log("new2:" + newRot);
        if (newRot >= maxDegree)
        {
            Debug.Log("Предел:" + newRot);
            newRot = maxDegree;
        }
        if (newRot <= -maxDegree)
        {
            Debug.Log("Предел2");
            newRot = -maxDegree;
        }
        if (!car)
        {
            Debug.Log("rotation:" + newRot);
            transform.rotation = Quaternion.Euler(0, 0, newRot);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        Vector3 newpos = transform.right * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(transform.position + newpos);
        if (transform.position.y < -2.5f)
        {
            jet = false;
            car = false;
            drel = true;
        }
        if (transform.position.y > 2.5f)
        {
            jet = true;
            car = false;
            drel = false;
        }
        if (transform.position.y < -1f)
        {
            if (!dirts[0].enabled)
            {
                for (int i = 0; i < dirts.Length; i++)
                {
                    dirts[i].enabled = true;
                }
            }
        }
        else
        {
            if (dirts[0].enabled)
            {
                for (int i = 0; i < dirts.Length; i++)
                {
                    dirts[i].enabled = false;
                }
            }
        }
    }
    public void set()
    {
        Debug.Log("Setttttt");
        loose.SetActive(true);
    }
}
