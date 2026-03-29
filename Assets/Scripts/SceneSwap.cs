using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    [SerializeField] private InputActionReference _swapAction;
    
    [HideInInspector] public UnityEvent OnSwapAction;
    
    void Start()
    {
        OnSwapAction ??= new UnityEvent();
        OnSwapAction.AddListener(SwapToGameplay);
    }

    void Update()
    {
        if (_swapAction.action.WasPressedThisFrame())
        {
            OnSwapAction.Invoke();
        }
    }

    private void SwapToGameplay()
    {
        SceneManager.LoadScene("AssignmentScene");
    }
}
