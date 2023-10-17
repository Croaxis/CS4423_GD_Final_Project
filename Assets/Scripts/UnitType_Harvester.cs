using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class UnitType_Harvester : MonoBehaviour, IUnitData
{
    public string unitType = "Harvester";
    public float hp = 100f;
    public float moveSpeed = 5f;
    public float attackSpeed = 0f;
    //public string spritePath = "Sprites/Harvester.png";
    public Sprite unitSprite;

    private void Awake()
    {
        //LoadSprite();
        // var validateAddress = Addressables.LoadResourceLocationsAsync(spritePath);
        // validateAddress.Completed += OnResourceLocationsLoaded;
    }

    // private void OnResourceLocationsLoaded(AsyncOperationHandle<IList<IResourceLocation>> handle)
    // {
    //     if (handle.Status == AsyncOperationStatus.Succeeded)
    //     {
    //         foreach (var location in handle.Result)
    //         {
    //             Addressables.LoadAssetAsync<Sprite>(location).Completed += op =>
    //             {
    //                 if (op.Status == AsyncOperationStatus.Succeeded)
    //                 {
    //                     unitSprite = op.Result;
    //                     setSprite(unitSprite);
    //                 }
    //                 else
    //                 {
    //                     Debug.LogError("Failed to load sprite: " + op.DebugName);
    //                 }
    //             };
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("Failed to load resource locations: " + handle.DebugName);
    //     }
    // }

    // private Sprite setSprite(Sprite unitSprite){
    //     return this.unitSprite = unitSprite;
    // }

    // private void LoadSprite()
    // {
    //     unitSprite = Resources.Load<Sprite>(spritePath);
    //     if (unitSprite == null)
    //     {
    //         Debug.LogError("Failed to load sprite at path: " + spritePath);
    //     }
    // }

    public string GetUnitType() { return unitType; }
    public float GetHP() { return hp; }
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetAttackSpeed() { return attackSpeed; }
    public Sprite GetUnitSprite() { return unitSprite; }
}