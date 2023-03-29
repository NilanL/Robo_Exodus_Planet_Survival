using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Camera_Focus : MonoBehaviour
{
    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        var cameraObject = GameObject.Find("ParentCamera/Main Camera");
        _camera = cameraObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(_camera.transform.rotation * Vector3.forward);
        //transform.LookAt(_camera.transform);
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }
}
