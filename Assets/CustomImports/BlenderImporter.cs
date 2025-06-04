using UnityEngine;
using UnityEditor; 

public class BlenderImporter : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        ModelImporter importer = assetImporter as ModelImporter;
        importer.animationType = ModelImporterAnimationType.Human; 
    }
}
