using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputSystem _input;
    [SerializeField] private LayerMask platformLayer;
    private void Awake(){
        #if UNITY_EDITOR
            _input = gameObject.AddComponent<MouseInput>();
            _input.SetLayer(platformLayer);
        #else
            _input = gameObject.AddComponent<TouchInput>();
            _input.SetLayer(platformLayer);
        #endif
    }

    private void Update(){
        _input.UpdateInput();
    }
}
