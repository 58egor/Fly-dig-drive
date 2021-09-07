using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Camera cam;
    public GameObject player;
    public float x;
    public float minY;
    public float maxY;
    bool pause=false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null && !pause)
        cam.transform.position = new Vector3(player.transform.position.x+x, Mathf.Clamp(player.transform.position.y,minY,maxY), cam.transform.position.z);
    }
    public void stop()
    {
        pause = true;
    }
}
