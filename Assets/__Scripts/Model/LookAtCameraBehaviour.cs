using UnityEngine;

public class LookAtCameraBehaviour : MonoBehaviour
{
    public Transform MainCamera { get; set; }

    void Update()
    {
        if (MainCamera == null) return;
        transform.rotation = MainCamera.rotation;
    }
}
