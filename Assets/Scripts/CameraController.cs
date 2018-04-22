using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject Demon;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Demon.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Demon.transform.position + offset;
    }
}
