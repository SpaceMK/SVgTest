using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour,ICameraComponent
{
    [SerializeField] private ImageSelectionManager selectionManager;
    [SerializeField] private Camera sceneCamera;
    private CommandHandler commandHandler = new CommandHandler();

    public Camera GetSceneCamera()
    {
        return sceneCamera;
    }

    void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                ShootRaycast shootRaycast = new ShootRaycast(this, Input.GetTouch(0).position);
                commandHandler.ScreenInteraction(shootRaycast, selectionManager);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShootRaycast shootRaycast = new ShootRaycast(this, Input.mousePosition);
            commandHandler.ScreenInteraction(shootRaycast, selectionManager);
        }

        
    }

}
