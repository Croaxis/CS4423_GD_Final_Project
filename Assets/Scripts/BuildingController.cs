using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour, ISelection
{
    private GameObject selectedGameObject;
    private bool isSelected = false;
    
    private void Awake()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        SetSelectedVisible(false);

        // InspectorController inspectorController = FindObjectOfType<InspectorController>();
        // if (inspectorController != null)
        // {
        //     inspectorController.SetElementImage(buildingData.GetUnitSprite());
        // }
    }

    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    // Method to check if the buidling is selected
    public bool IsSelected()
    {
        return isSelected;
    }

    // Method to set the selected state of the building.
    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
