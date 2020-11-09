using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 1;
    public float speedRotation = 1;
    private CharacterController chController;
    void Start()
    {
        chController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,Input.GetAxis("Horizontal")*speedRotation, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        chController.SimpleMove(forward * curSpeed);
    }
}
