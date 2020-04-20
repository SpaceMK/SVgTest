using System.IO;
using Unity.VectorGraphics;
using UnityEngine;
using static Unity.VectorGraphics.SVGParser;

public class VectorImageManager : MonoBehaviour
{
    [SerializeField] private VectorImageData imageData;
    [SerializeField] private GameObject coloringAreaPrifab;
    [SerializeField] private SpriteRenderer outlineSpriteRenderer,effectsSpriteRendered;
    private SVGLoader svgLoader = new SVGLoader();
    private SceneInfo vectorImageNodes;
   
    void Start()
    {
        LoadSVGCode();
    }

    private void LoadSVGCode()
    {
        vectorImageNodes = SVGParser.ImportSVG(new StringReader(imageData.SVGImageXML));
        AddToVectorImageData();
        CreateColoringAreas();
        AddVectorEffects();
    }

    private void CreateColoringAreas()
    {
        foreach (string tag in imageData.ColoringAreaTag)
        {
            GameObject coloringArea = Instantiate(coloringAreaPrifab,Vector3.zero,Quaternion.identity);
            coloringArea.transform.SetParent(transform);
            coloringArea.transform.localScale = new Vector3(1f,1f,1f);
            IColorableImage colorableImage = coloringArea.GetComponent(typeof(IColorableImage)) as IColorableImage;
            if (colorableImage == null)
                return;
            colorableImage.SetDependencies(tag,vectorImageNodes.NodeIDs[tag],imageData);
            colorableImage.CreateSVG();
        }
    }

    private void AddToVectorImageData()
    {
        foreach (var tag in imageData.ColoringAreaTag)
        {
            imageData.ColoringArea.Add(tag, vectorImageNodes.NodeIDs[tag]);
            imageData.OriginalShapeFill.Add(vectorImageNodes.NodeIDs[tag].Children[0].Shapes[0].Fill);
        }    
    }

    private void AddVectorEffects()
    {
        if(!string.IsNullOrEmpty(imageData.effectsTag)|| effectsSpriteRendered!=null)
        effectsSpriteRendered.sprite = svgLoader.LoadSVG(vectorImageNodes.NodeIDs[imageData.effectsTag]);

        if(!string.IsNullOrEmpty(imageData.outlinesTag)|| outlineSpriteRenderer!=null)
        outlineSpriteRenderer.sprite = svgLoader.LoadSVG(vectorImageNodes.NodeIDs[imageData.outlinesTag]);
    }

    private void OnApplicationQuit()
    {
        imageData.OriginalShapeFill.Clear();
        imageData.ColoringArea.Clear();
    }
}

