using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : ICommand
{
    public void ScreenInteraction(ShootRaycast shootRaycast, IImageSelect selectImage)
    { 
        selectImage.ImageClicked(shootRaycast.RaycastHit());
    }
}
