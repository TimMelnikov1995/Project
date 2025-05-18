using UnityEditor;
using UnityEditor.SceneManagement;

public class QuickAccess
{
    [MenuItem("QuickAccess/Main Scene", false, 1)]
    public static void MainScene()
    {
        string path = "Assets/_Scenes/MainScene.unity";
        EditorSceneManager.OpenScene(path);
    }

    [MenuItem("QuickAccess/Addressable Objects Containers Collection", false, 2)]
    public static void AddressableObjectsContainersCollection()
    {
        string path = "Assets/Resources/AddressableObjectsContainersCollection.asset";
        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(path);
    }
}