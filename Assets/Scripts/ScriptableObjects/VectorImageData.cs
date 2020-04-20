using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;


[CreateAssetMenu(fileName = "ImageData", menuName = "Vector Image Data", order = 1)]
public class VectorImageData : ScriptableObject
{
    public Sprite PreviewVectorImage;
    public Dictionary<string,SceneNode> ColoringArea = new Dictionary<string,SceneNode>();
    public List<IFill> OriginalShapeFill = new List<IFill>();


    [Header("SVG XML Area")]
    [Multiline]
    public string SVGImageXML;
    public string[] ColoringAreaTag;
    public string effectsTag;
    public string outlinesTag;
}





