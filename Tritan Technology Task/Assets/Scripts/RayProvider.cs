using UnityEngine;

public class RayProvider : MonoBehaviour, IRayProvider
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public Ray CreateRay()
    {
        return _camera.ScreenPointToRay(Input.mousePosition);
    }
}