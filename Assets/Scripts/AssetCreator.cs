using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.U2D;
using Unity.Collections;
using System.Linq;
using UnityEditor;


public class AssetCreator : MonoBehaviour
{
    public int skinIndex;

    [Space(30)]
    public SpriteLibrary spriteLibrary = default;

    [Header("   Rigged, Animated Character")]
    public GameObject targetReference;  // SpriteResolver script reference

    [Header("   Unrigged Character with Sprites")]
    public GameObject targetSprites;  // Sprites to output

    public SpriteInput[] NewSprite;


// Struct / Methods / Enums

    public enum PartCategory
    {
        Head,
        Body,
        Arm_R,
        Arm_L,
        Hand_R,
        Hand_L,
        Leg_R,
        Leg_L,
        Foot_R,
        Foot_L,
        
        None
    }

    [System.Serializable]
    public class SpriteInput
    {
        public Sprite sprite;
        public PartCategory targetCategory;
        public SpriteResolver targetResolver;
    }

    private SpriteLibraryAsset LibraryAsset => spriteLibrary.spriteLibraryAsset;

// Functions

    public void CustomAsset(SpriteInput customSprite)
    {
        // Duplicate bones and poses
        string referenceLabel = customSprite.targetResolver.GetLabel();
        Sprite referenceSprite = spriteLibrary.GetSprite(customSprite.targetCategory.ToString(), referenceLabel);
        SpriteBone[] bones = referenceSprite.GetBones();
        NativeArray<Matrix4x4> poses = referenceSprite.GetBindPoses();
        customSprite.sprite.SetBones(bones);
        customSprite.sprite.SetBindPoses(poses);
        
        // Name
        string[] labelList = LibraryAsset.GetCategoryLabelNames(customSprite.targetCategory.ToString()).ToArray();
        string customLabel = labelList[labelList.Length - 1];
        string endSubString = (int.Parse(customLabel.Substring(customLabel.Length - 2, 2)) + 1).ToString();
        if (endSubString.Length < 2) endSubString = string.Concat("0", endSubString);
        customLabel = customLabel.Substring(0, customLabel.Length - 2) + endSubString;

        // Add Asset to Library
        LibraryAsset.AddCategoryLabel(customSprite.sprite, customSprite.targetCategory.ToString(), customLabel);
        //customSprite.targetResolver.SetCategoryAndLabel(customSprite.targetCategory.ToString(), customLabel);
    }

// Button Called
    public void NewAsset()
    {
        SpriteLibrary refLibrarySprites = targetSprites.GetComponent<SpriteLibrary>();
        int loop = (refLibrarySprites != null) ? refLibrarySprites.spriteLibraryAsset.GetCategoryLabelNames("Head").Count() : 1;

        for (int L = 0; L < loop; L++)
        {
            if (refLibrarySprites != null) GetInputs(refLibrarySprites.spriteLibraryAsset, L);
            else GetInputs();

            for (int i = 0; i < NewSprite.Length; i++)
            {
                var input = NewSprite[i];

                if (input.sprite != null && input.targetCategory != PartCategory.None && input.targetResolver != null)
                {
                    CustomAsset(input);
                }
                else Debug.Log("SpriteInput incomplete !");

                //targetSprites = null;  // Empty Sprites slot
            }
        }
    }

    private void GetInputs()
    {
        for (int i = 0; i < NewSprite.Length; i++)
        {
            var input = NewSprite[i];

            Transform resolverTransform = targetReference.transform.Find(input.targetCategory.ToString());
            Transform spriteTransform = targetSprites.transform.Find(input.targetCategory.ToString());

            if (resolverTransform != null && spriteTransform != null)
            {
                input.targetResolver = resolverTransform.GetComponent<SpriteResolver>();
                input.sprite = spriteTransform.GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                if (resolverTransform == null) Debug.Log("SpriteResolver " + input.targetCategory.ToString() + " not found !");
                if (spriteTransform == null) Debug.Log("Sprite for " + input.targetCategory.ToString() + " not found !");
            }
        }
    }

    private void GetInputs(SpriteLibraryAsset libraryAsset, int index)
    {
        for (int i = 0; i < NewSprite.Length; i++)
        {
            var input = NewSprite[i];

            Transform resolverTransform = targetReference.transform.Find(input.targetCategory.ToString());

            if (resolverTransform != null && libraryAsset.GetCategoryLabelNames(input.targetCategory.ToString()).Count() > 0)
            {
                input.targetResolver = resolverTransform.GetComponent<SpriteResolver>();

                string label = libraryAsset.GetCategoryLabelNames(input.targetCategory.ToString()).ToArray()[index];
                Sprite librarySprite = libraryAsset.GetSprite(input.targetCategory.ToString(), label);
                input.sprite = librarySprite;
            }
            else
            {
                if (resolverTransform == null) Debug.Log("SpriteResolver " + input.targetCategory.ToString() + " not found !");
                if (libraryAsset.GetCategoryLabelNames(input.targetCategory.ToString()).Count() <= 0) Debug.Log("Sprite for " + input.targetCategory.ToString() + " not found !");
            }
        }
    }



    public void ChangeToIndex()
    {
        string[] categories = LibraryAsset.GetCategoryNames().ToArray();

        for (int i = 0; i < categories.Length; i++)
        {
            string C = categories[i];

            Transform part = targetReference.transform.Find(C);
            string[] spriteList = LibraryAsset.GetCategoryLabelNames(C).ToArray();
            Sprite spr = (spriteList.Length > skinIndex) ? LibraryAsset.GetSprite(C, spriteList[skinIndex]) : null;

            if (spr != null && part != null) part.GetComponent<SpriteRenderer>().sprite = spr;
        }
    }
}


// Editor Script

[CustomEditor(typeof(AssetCreator))]
class AssetCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AssetCreator myScript = (AssetCreator)target;

        if (GUILayout.Button("Index Skin", GUILayout.Width(150), GUILayout.Height(20)))
        {
            myScript.ChangeToIndex();
        }

        DrawDefaultInspector();

        /*if (GUILayout.Button("GET Assets", GUILayout.Width(250), GUILayout.Height(30)))
        {
            myScript.GetInputs();
        }*/

        if (GUILayout.Button("Create Asset", GUILayout.Width(250), GUILayout.Height(30)))
        {
            myScript.NewAsset();
        }
    }
}
