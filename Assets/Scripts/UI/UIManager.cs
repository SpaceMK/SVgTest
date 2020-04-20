using System;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject colorPalettPanel,buttonHolder;
    [SerializeField] private Button[] colorPalletBtn;
    [SerializeField] private SVGImage svgButton;

    private int selectedColorIndex=-1;
   
    private SVGImage image;
    private IColorableImage colorableImage;
    public IFill CurrentSelectedColor { private set; get; }
    public Action DeselectImage;
    
    void Start()
    {
        colorPalettPanel.SetActive(false);
    }

    public void AddListenersToGenericButton(IColorableImage colorableImage)
    {
        for (int i = 0; i < colorPalletBtn.Length; i++)
        {
            SolidFill fill = new SolidFill() { };
            fill.Color = colorPalletBtn[i].GetComponent<SVGImage>().color;
            colorPalletBtn[i].onClick.AddListener(() => SetCurrentSelectedColor(fill));
        }
    }

    public void ActivatePalettPanel(IColorableImage colorable,int index)
    {
        if (selectedColorIndex == index)
            return;

       CurrentSelectedColor = null;
       colorPalettPanel.SetActive(true);
       selectedColorIndex = index;
       colorableImage = colorable;
       var fill = colorableImage.GetImageData().OriginalShapeFill[index];
       CreateColorOptions(fill);
    }

    private void CreateColorOptions(IFill fill)
    {
        if (fill.GetType() == typeof(SolidFill))
        {
            svgButton.sprite = null;
            svgButton.color = ((SolidFill)fill).Color;
        }
        else
            CreateUIGradient(svgButton,fill);
        colorPalletBtn[0].onClick.RemoveAllListeners();
        colorPalletBtn[0].onClick.AddListener(() => SetCurrentSelectedColor(fill));
    }

    private void SetCurrentSelectedColor(IFill color)
    {
        CurrentSelectedColor = color;
    }

    private void CreateUIGradient(SVGImage image,IFill fill)
    {
        Shape rectangle = new Shape();
        VectorUtils.MakeRectangleShape(rectangle, new Rect(0, 0, 100, 100));
        rectangle.Fill = fill;
        rectangle.PathProps = new PathProperties()
        {
            Stroke = new Stroke() { Color = Color.white }
        };
        Scene scene = new Scene()
        {
            Root = new SceneNode() { Shapes = new List<Shape> { rectangle } }
        };
        image.sprite = new SVGLoader().LoadSVG(scene.Root);
    }

    public void ClosePalettPanel()
    {
        CurrentSelectedColor = null;
        selectedColorIndex = 0;
        colorPalettPanel.SetActive(false);
        DeselectImage?.Invoke();
    }
}
