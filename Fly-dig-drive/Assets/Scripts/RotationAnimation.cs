using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    public float x;
    public float y;
    public float z;
    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + x*Time.deltaTime, transform.rotation.eulerAngles.y + y * Time.deltaTime, transform.rotation.eulerAngles.z + z * Time.deltaTime);
        transform.Rotate(new Vector3(x, y, z));
    }
}
