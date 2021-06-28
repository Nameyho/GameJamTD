using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region properties

   
    public float Speed;
    

    #endregion properties



    #region fields

    private Transform _playerTransform;
    private float _speed;
    
    #endregion fields



    #region public methods
    
    #endregion public methods



    #region unity messages
    private void Awake()
    {
        
        
    }

    private void Start()
    {
        _playerTransform = transform ;
        _speed = Speed;
    }

    private void Update()
    {
       
        
    }

    private void LateUpdate()
    {
        DetectInput();
    }


    #endregion unity messages



    #region private methods

    private void DetectInput()
    {
      
        float AxisX = Input.GetAxis("Horizontal");
        float AxisZ = Input.GetAxis("Vertical");
        
        Vector3 movement = Vector3.zero;

        movement = new Vector3(AxisX, 0f, AxisZ);

        Vector3 newdir = _playerTransform.TransformDirection(movement);

        

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed /= 2;
        }

        _playerTransform.position += newdir * _speed * Time.deltaTime;
    }

    #endregion private methods
}
