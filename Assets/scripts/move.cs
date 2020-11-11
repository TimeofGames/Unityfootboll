using UnityEngine;
using System;
//эти строчки гарантирют что наш скрипт не завалится если на плеере будет отсутствовать нужные компоненты
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class move : MonoBehaviour
{
    public float Speed = 0.3f;
    public GameObject GameObjectCamera;
    private Camera _camera;
    //даем возможность выбрать тэг пола.
    //так же убедитесь что ваш Player сам не относится к даному слою. 

    //!!!!Нацепите на него нестандартный Layer, например Player!!!!
    public LayerMask GroundLayer = 1; // 1 == "Default"

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

        //т.к. нам не нужно что бы персонаж мог падать сам по-себе без нашего на то указания.
        //то нужно заблочить поворот по осях X и Z
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        //  Защита от дурака
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
        // т.к. мы сейчас решили использовать физическое движение снова,
        // мы убрали и множитель Time.fixedDeltaTime
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