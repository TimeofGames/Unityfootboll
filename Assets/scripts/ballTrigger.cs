using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;

public class ballTrigger : MonoBehaviour
{
    public new GameObject gameObjectScore;
    public new GameObject gameObjectBall;
    public new GameObject gameObjectPlayer;
    private Text _output;
    private Vector3 _startPosPlayer = new Vector3(4.5f, 3.3f, -36f); 
    private Quaternion _startRotPlayer = new Quaternion(0f, -1f, 0f,1); 
    private Vector3 _startPosBall = new Vector3(4.5f, 3.3f, -34f); 

    private void Start()
    {
        _output = gameObjectScore.GetComponent<Text>();
    }
    

    void OnTriggerEnter(Collider myTrigger) 
    {
        if (myTrigger.gameObject.name == "ball")
        {
            _output.text=(Convert.ToInt32(_output.text)+1).ToString();
            gameObjectPlayer.GetComponent<Transform>().transform.position  = _startPosPlayer;
            gameObjectPlayer.GetComponent<Transform>().transform.rotation  = _startRotPlayer;
            
            gameObjectPlayer.GetComponent<Rigidbody>().isKinematic = true;
            gameObjectPlayer.GetComponent<Rigidbody>().isKinematic = false;
            
            gameObjectBall.GetComponent<Transform>().transform.position  = _startPosBall;
            gameObjectBall.GetComponent<Rigidbody>().isKinematic = true;
            gameObjectBall.GetComponent<Rigidbody>().isKinematic = false;

        }
    }
}
