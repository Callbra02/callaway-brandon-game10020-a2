using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private GameObject _playerReference;
    [SerializeField] private InputActionReference _lookAction;
    [SerializeField] private float _cameraLerpSpeed = 2.0f;
    
    void Start()
    {
        _playerReference = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 newPosition =  new Vector3(
            _playerReference.transform.position.x,
            _playerReference.transform.position.y, 
            this.transform.position.z); 
        
        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, _cameraLerpSpeed * Time.deltaTime);
    }
}
