using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;

public class SVGLoader 
{
    public Sprite LoadSVG(SceneNode node)
    {
        Scene vectorScene = new Scene() { };
        vectorScene.Root = node;
        var tessOptions = new VectorUtils.TessellationOptions()
        {
            StepDistance = 100.0f,
            MaxCordDeviation = 0.5f,
            MaxTanAngleDeviation = 0.1f,
            SamplingStepSize = 0.01f
        };
        var geoms = VectorUtils.TessellateScene(vectorScene, tessOptions);
        var sprite = VectorUtils.BuildSprite(geoms, 10.0f, VectorUtils.Alignment.SVGOrigin, Vector2.zero, 128, true);
        return sprite;
    }
}
