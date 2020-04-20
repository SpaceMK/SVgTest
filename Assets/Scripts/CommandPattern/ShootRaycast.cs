using Unity.VectorGraphics;
using UnityEngine;
public class ShootRaycast
{
    private Camera rayCastCamera;
    private Vector3 screenInputPosition;
    public Vector2 RayCastPointMatrix { private set; get; }
    public ShootRaycast(ICameraComponent camera, Vector3 clickPosition)
    {
        rayCastCamera = camera.GetSceneCamera();
        screenInputPosition = clickPosition;
    }


    public GameObject RaycastHit()
    {
        Ray ray = rayCastCamera.ScreenPointToRay(screenInputPosition);
         RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
            return hit.collider.gameObject;
        else
            return null;

    }   
}


