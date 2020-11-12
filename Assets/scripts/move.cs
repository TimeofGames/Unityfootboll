using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class move : MonoBehaviour
{
    public float Speed = 0.3f;
    public GameObject GameObjectCamera;
    private Camera _camera;
  
    public LayerMask GroundLayer = 1; 

    private Rigidbody _rb;
    private Transform _transform;

    private Vector3 _movementVector
    {
        get
        {
            if (Input.GetKey(KeyCode.W))
            {
                return _transform.right;
            }

            if (Input.GetKey(KeyCode.S))
            {
                return -_transform.right;
            }

            if (Input.GetKey(KeyCode.A))
            {
                return _transform.forward;
            }

            if (Input.GetKey(KeyCode.D))
            {
                return -_transform.forward;
            }

            return Vector3.zero;
        }
    }


    void Start()
    {
        _camera = GameObjectCamera.GetComponent<Camera>();
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();

        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        
        if (GroundLayer == gameObject.layer)
            Debug.LogError("Player SortingLayer must be different from Ground SourtingLayer!");
    }

    void FixedUpdate()
    {
        MoveLogic();
        RotateLogic();
    }


    private void MoveLogic()
    {

        _rb.AddForce(_movementVector * Speed, ForceMode.Impulse);
    }
    

    private void RotateLogic()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast (ray, out hit))
            {
                Vector3 rot = transform.eulerAngles;
                transform.LookAt(hit.point);
                transform.eulerAngles = new Vector3(rot.x, transform.eulerAngles.y-90, rot.z);
            }
        }
    }

}