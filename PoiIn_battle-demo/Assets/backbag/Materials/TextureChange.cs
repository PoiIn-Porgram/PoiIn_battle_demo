using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class TextureChange : AssetPostprocessor
{
    private void OnPreprocessTexture() {
        //引入assetImporter(所有导入器的父类)转换为TextureImporter导入器
        TextureImporter importer = (TextureImporter)assetImporter;
        //将所有导入图片设置成 Spritre 类型
        importer.textureType = TextureImporterType.Sprite;
    }
}