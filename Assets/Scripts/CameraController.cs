using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _playerReference;
    [SerializeField] private float _cameraLerpSpeed = 2.0f;
    
    void Start()
    {
        _playerReference = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(
            _playerReference.transform.position.x,
            _playerReference.transform.position.y, this.transform.position.z), _cameraLerpSpeed * Time.deltaTime);
    }
}
