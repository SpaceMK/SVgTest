using Unity.VectorGraphics;
using UnityEngine;
public interface IColorableImage
{
    VectorImageData GetImageData();
    void SetColorToImage(IFill newFill);
    void SetDependencies(string id,SceneNode node,VectorImageData data);
    void CreateSVG();
    string GetID();
}

