using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Variables
    public Transform player;
    public float smooth = 0.3f;

    private Vector3 velocity = Vector3.zero;

    //Methods
    void Update ()
    {
        Vector3 pos = new Vector3();
        pos = player.position;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);

    }
}
