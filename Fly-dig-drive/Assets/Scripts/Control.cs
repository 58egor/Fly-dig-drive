using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Control : MonoBehaviour
{
    Rigidbody rigidbody;
    bool gunActive = false;
    public GameObject loose;
    public GameObject[] guns;
    public GameObject[] objects;
    public TrailRenderer[] planeTrail;
    public TrailRenderer[] carTrail;
    public TrailRenderer[] drelTrail;
    public TrailRenderer[] nitro;
    public Rigidbody bullet;
    public GameObject crater;
    public float gunCount = 0.5f;
    float count = 0;
    public float carSpeed;
    public float drelSpeed;
    public float planeSpeed;
    float speed = 40;
    int layerMask;
    public float speedGun = 20f;
    float rotSpeed;
    public float maxDegree = 70;
    public float rotationSpeed = 10f;
    bool car = true;
    bool jet = false;
    bool drel = false;
    bool onGround = false;
    Vector2 startPos;
    Vector2 direction;
    bool directionChosen;
    float stun = 0.5f;
    float stunActive=0;
    float turbo=0;
    float turboTime;
    bool start = false;
    AudioManager auido;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {        if (other.gameObject.layer == 4 && !drel)
        {
            jet = false;
            car = false;
            drel = true;//будм дрелью
            Instantiate(crater, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -90)));
            rotSpeed = rotationSpeed;
        }
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
        
        if (other.gameObject.layer == 4)
        {
            if (drel)
            {
                RaycastHit hit;
                int layerMask1 = 1 << 10;
                if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1f, layerMask1))
                {
                    Instantiate(crater, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                }
                else
                {
                    Instantiate(crater, new Vector3(transform.position.x-2, transform.position.y - 1f, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -90)));
                   
                }
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
        auido = gameObject.GetComponent<AudioManager>();
        layerMask = 1 << 11;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rotSpeed = rotationSpeed;
    }
    private void Update()
    {
        if (start)
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
                if ((car && onGround) || drel)//и в это время были машиной
                {
                    car = false;
                    if (!drel)
                    {
                        Instantiate(crater, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 180)));
                        drel = true;//будм дрелью
                    }
                    rigidbody.isKinematic = false;
                }
                rotSpeed = -rotationSpeed;
            }
            if (direction.y > 0.1f && !directionChosen)
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
                setCar();
               
            }
            if (jet)
            {
                if (!auido.isPlaying("Plane"))
                    auido.Play("Plane");
                auido.Stop("Drel");
                auido.Stop("Car");
                speed = planeSpeed;
                for (int i = 0; i < planeTrail.Length; i++)
                {
                    planeTrail[i].emitting = true;
                }
                for (int i = 0; i < carTrail.Length; i++)
                {
                    carTrail[i].emitting = false;
                }
                for (int i = 0; i < drelTrail.Length; i++)
                {
                    drelTrail[i].emitting = false;
                }
                rigidbody.velocity = new Vector3(0, 0, 0);
                rigidbody.useGravity = false;
                objects[0].SetActive(true);
                objects[1].SetActive(false);
                objects[2].SetActive(false);
            }
            if (drel)
            {
                auido.Stop("Plane");
                if(!auido.isPlaying("Drel"))
                auido.Play("Drel");
                auido.Stop("Car");
                speed = drelSpeed;
                for (int i = 0; i < planeTrail.Length; i++)
                {
                    planeTrail[i].emitting = false;
                }
                for (int i = 0; i < carTrail.Length; i++)
                {
                    carTrail[i].emitting = false;
                }
                for (int i = 0; i < drelTrail.Length; i++)
                {
                    drelTrail[i].emitting = true;
                }
                rigidbody.velocity = new Vector3(0, 0, 0);
                rigidbody.useGravity = false;
                objects[0].SetActive(false);
                objects[1].SetActive(false);
                objects[2].SetActive(true);
            }
            if (gunActive)
            {
                Fire();
            }
        }
    }
    void Fire()
    {
        Vector3 pos=transform.position;
        if(drel)
         pos= guns[2].transform.position;
        if (car)
        pos = guns[1].transform.position;
        if (jet)
            pos = guns[0].transform.position;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.right);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && count <= 0)
        {
            Debug.Log("Name:" + hit.collider.name);
            Debug.Log("Pos:" + hit.point);
            Instantiate(bullet,pos, Quaternion.identity);
            count = gunCount;
        }
        else
        {
            count -= Time.deltaTime;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (start)
        {
            Debug.Log("Rot:" + rotSpeed);
            float newRot = transform.rotation.eulerAngles.z + rotSpeed * Time.fixedDeltaTime;
            Debug.Log(newRot);
            if (newRot >= 360 - maxDegree - 10)
            {
                newRot -= 360;
                Debug.Log("new:" + newRot);
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
            Vector3 newpos = transform.right * (speed + turbo) * Time.fixedDeltaTime;
            rigidbody.MovePosition(transform.position + newpos);
            if (turbo != 0)
            {
                if (turboTime > 0)
                {
                    for (int i = 0; i < nitro.Length; i++)
                    {
                        nitro[i].emitting = true;
                    }
                    turboTime -= Time.deltaTime;
                }
                else
                {
                    for (int i = 0; i < nitro.Length; i++)
                    {
                        nitro[i].emitting = false;
                    }

                    turbo = 0;
                }
            }
            else
            {
                for (int i = 0; i < nitro.Length; i++)
                {
                    nitro[i].emitting = false;
                }

            }
        }
    }
    public void set()
    {
        Debug.Log("Setttttt");
        loose.SetActive(true);
    }
    public void set2()
    {
        Debug.Log("Setttttt2");
        gunActive = true;
    }
    public void setCar()
    {
        auido.Stop("Plane");
        auido.Stop("Drel");
        if (!auido.isPlaying("Car"))
            auido.Play("Car");
        for (int i = 0; i < planeTrail.Length; i++)
        {
            planeTrail[i].emitting = false;
        }
        for (int i = 0; i < carTrail.Length; i++)
        {
            carTrail[i].emitting = true;
        }
        for (int i = 0; i < drelTrail.Length; i++)
        {
            drelTrail[i].emitting = false;
        }
        rigidbody.useGravity = true;
        objects[0].SetActive(false);
        objects[1].SetActive(true);
        objects[2].SetActive(false);
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out hit, 2))
        {
            Debug.Log("hit:" + hit.collider.name);
            if (hit.collider.gameObject.layer == 10)
            {
                onGround = true;
                speed = carSpeed;
            }
            else
            {
                speed = planeSpeed;
                onGround = false;
            }
        }
        else
        {
            speed = planeSpeed;
            onGround = false;
        }
    }
    public void SetTurbo(float tur,float turTmime)
    {
        turbo = tur;
        turboTime = turTmime;

    }
    public void SetStart()
    {
        start = true;

    }
}
