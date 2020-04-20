using System;
using UnityEngine;

public class ImageSelectionManager : MonoBehaviour,IImageSelect
{
    [SerializeField] private UIManager uiManager;
    private IColorableImage currentImageSelected;
    private int currentSelectedSceneNodeIndex=-1;

    void Start()
    {
        uiManager.DeselectImage += ObjectDeselected;
    }

    public void ImageClicked(GameObject imageClicked)
    {
        if (imageClicked == null)
            return;
        IColorableImage colorableImage = imageClicked.GetComponent(typeof(IColorableImage)) as IColorableImage;
        if (colorableImage != null)
            ImageSelected(colorableImage);
        else
            Debug.Log("Not a vector image");
    }

    private void ImageSelected(IColorableImage colorableImage)
    {
        if (currentImageSelected == null || currentImageSelected != colorableImage)
        {
            uiManager.AddListenersToGenericButton(colorableImage);
            currentImageSelected = colorableImage;
            currentSelectedSceneNodeIndex = Array.IndexOf(colorableImage.GetImageData().ColoringAreaTag, colorableImage.GetID());
            uiManager.ActivatePalettPanel(colorableImage, currentSelectedSceneNodeIndex);
        }
        else
        {
            PaintImage();
        }
    }

    private void PaintImage()
    {
        if (uiManager.CurrentSelectedColor == null || currentSelectedSceneNodeIndex<0)
            return;
        currentImageSelected.SetColorToImage(uiManager.CurrentSelectedColor);
    }

    private void ObjectDeselected()
    {
        currentImageSelected = null;
        currentSelectedSceneNodeIndex = -1;
    }
}
