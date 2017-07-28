using UnityEngine;
using UnityEditor;

public class ScriptableAssetCreator
{
    [MenuItem("Stats/UnitStats/RunStat")]
    public static void CreateRunStat()
    {
        ScriptableObjectUtility.CreateAsset<RunStat>();
    }

    //[MenuItem("Assets/Create/AnimationDatabase")]
    //public static void CreateAnimationDatabase()
    //{
    //    ScriptableObjectUtility.CreateAsset<AnimationDataBase>();
    //}
}