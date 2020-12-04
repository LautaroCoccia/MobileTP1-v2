//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class InputManager : MonoBehaviour
//{
//    const int MyGiro = 30;
//    const int speed = 2000;
//    public LevelManager lvlManager;
//    public GameObject Player1;
//    private Rigidbody Player1Rigid;
//    public GameObject Player2;
//    private Rigidbody Player2Rigid;
//    static InputManager instance = null;
//    public static InputManager Instance
//    {
//        get
//        {
//            if(instance==null)
//            {
//                instance = new InputManager();
//            }
//            return instance;
//        }

//    }
//    // Update is called once per frame
//    private void Start()
//    {
//        lvlManager = GetComponent<LevelManager>();
//        Player1Rigid= Player1.GetComponent<Rigidbody>();
//        Player2Rigid=Player2.GetComponent<Rigidbody>();
//    }
    
//    void Update()
//    {
//#if UNITY_EDITOR
//        Player2Rigid.AddForce(0, 0, 100, ForceMode.Acceleration);
//        Player1Rigid.AddForce(0, 0, 100, ForceMode.Acceleration );
//        //Player2Rigid.AddForce(0, 0, speed);
//        if (Input.GetKey(KeyCode.A))
//        {
//            Player1.transform.Rotate(new Vector3(GetAxis("Horizontal"), -MyGiro * Time.deltaTime));
//        }
//        else if (Input.GetKey(KeyCode.D))
//        {
//            Player1.transform.Rotate(new Vector3(GetAxis("Horizontal"), MyGiro * Time.deltaTime));
//        }
//        if (Input.GetKey(KeyCode.LeftArrow) )
//        {
//            Player2.transform.Rotate(new Vector3(GetAxis("Horizontal2"), -MyGiro * Time.deltaTime));
//        }
//        else if(Input.GetKey(KeyCode.RightArrow))
//        {
//            Player2.transform.Rotate(new Vector3(Input.GetAxis("Horizontal2"), MyGiro * Time.deltaTime));
//        }


//#elif UNITY_ANDROID
        
//#endif

//    }
//    Dictionary<string, float> axisValues = new Dictionary<string, float>()
//    {
//        { "Horizontal",0f},
//        {"Vertical", 0f }
//    };
//    public void SetAxis(string axis, float  value)
//    {
//        axisValues[axis] = value;
//    }
//    public float GetAxis(string axis)
//    {
//#if UNITY_STANDALONE || UNITY_EDITOR
        
//      return Input.GetAxis(axis);
//#elif UNITY_ANDROID
//        return axisValues[axis];
//#endif

//    }
//    public bool GetButton(string button)
//    {
//        return Input.GetButton(button);
//    }
    
//}
