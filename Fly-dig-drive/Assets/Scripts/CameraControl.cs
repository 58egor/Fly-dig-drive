using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Camera cam;
    public GameObject player;
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(player.transform.position.x+x, player.transform.position.y, cam.transform.position.z);
    }
}
