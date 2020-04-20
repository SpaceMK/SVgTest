using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;

public class ColorableImage : MonoBehaviour, IColorableImage
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PolygonCollider2D polygonCollider;

    private SVGLoader svgLoader = new SVGLoader();
    private VectorImageData imageData;
    private SceneNode areaSceneNode;
    private string areaID;

    public void SetDependencies(string id, SceneNode node,VectorImageData data)
    {
        imageData = data;
        gameObject.name = id;
        areaID = id;
        areaSceneNode = node;
    }

    public void CreateSVG()
    {
        PaintArea(new SolidFill { Color = Color.white });
        spriteRenderer.sprite = svgLoader.LoadSVG(areaSceneNode);
        SetColiiderSize();
    }

    private void SetColiiderSize()
    {
        polygonCollider.offset = Vector2.zero;
        List<Vector2> colliderPoints = new List<Vector2>();
        foreach (var segment in areaSceneNode.Children[0].Shapes[0].Contours[0].Segments)
        {
            colliderPoints.Add(new Vector2(segment.P0.x / 10, segment.P0.y / -10f));
            colliderPoints.Add(new Vector2(segment.P1.x / 10, segment.P1.y / -10f));
            colliderPoints.Add(new Vector2(segment.P2.x / 10, segment.P2.y / -10f));
        }
        polygonCollider.points = colliderPoints.ToArray();
    }

    private void PaintArea(IFill newColor)
    {
        foreach (var child in areaSceneNode.Children)
            child.Shapes[0].Fill = newColor;
    }

    public VectorImageData GetImageData()
    {
        return imageData;
    }

    public void SetColorToImage(IFill newFill)
    { 
        PaintArea(newFill);
        spriteRenderer.sprite = svgLoader.LoadSVG(areaSceneNode);
    }

    public string GetID()
    {
        return areaID;
    }
}














