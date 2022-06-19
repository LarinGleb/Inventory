#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class VoidItem
{

    [MenuItem("Inventory/Add New Item", priority = -11)]
    private static void CreateVoidItem() {
        GameObject BasicItem = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>("Assets/Inventory/Prefabs/BasicItem.prefab"));
        BasicItem.name = "BasicItem";

        BasicItem.transform.localPosition = Vector3.zero;
        BasicItem.transform.localEulerAngles = Vector3.zero;
        Selection.activeGameObject = BasicItem;
    }

    [MenuItem("Inventory/Clear Item", priority = -12)]
    private static void ClearItem() {
        Selection.activeGameObject.GetComponent<Item>().ClearItem();

    }

    [MenuItem("Inventory/Delete All Items", priority = -13)]
    private static void DeleteAllItems() {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            MonoBehaviour.DestroyImmediate(item);
        }

    }
    [MenuItem("Inventory/Create Coin", priority = -13)]

    private static void CreateCoin() {
        GameObject BasicCoin= (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>("Assets/Inventory/Prefabs/BasicCoin.prefab"));

        BasicCoin.name = "BasicCoin";

        BasicCoin.transform.localPosition = Vector3.zero;
        BasicCoin.transform.localEulerAngles = Vector3.zero;
        Selection.activeGameObject = BasicCoin;
    }
}

#endif