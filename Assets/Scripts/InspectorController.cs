using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectorController : MonoBehaviour
{
    public Image elementImage;
    private BuildingController buildingController;
    public GameObject pfHarvester; // Prefab of the harvester

    private void Start()
    {
        GameController.Instance.OnSelectedUnitsChanged += UpdateInspectorUI;
        elementImage.gameObject.SetActive(false);
    }

    private void UpdateInspectorUI(List<UnitController> selectedUnits)
{
    if (selectedUnits.Count > 0)
    {
        IUnitData unitData = selectedUnits[0].GetUnitData();
        elementImage.sprite = unitData.GetUnitSprite();

        if (selectedUnits[0] is BuildingController)
        {
            elementImage.gameObject.SetActive(true);
            // Optionally, you can set other UI elements or perform additional actions for buildings here.
        }
        else
        {
            elementImage.gameObject.SetActive(false);
        }
    }
    else
    {
        elementImage.gameObject.SetActive(false);
    }
}


    public void SetElementImage(Sprite sprite)
    {
        elementImage.sprite = sprite;
    }

    public void BuildEntity()
    {
        if (buildingController != null)
        {
            // Instantiate the pfHarvester prefab at the building's position with the player tag
            GameObject harvester = Instantiate(pfHarvester, buildingController.transform.position, Quaternion.identity);
            harvester.tag = "Player"; // Set the tag to "Player"
        }
    }

    // Method to set the currently selected building controller
    public void SetBuildingController(BuildingController building)
    {
        buildingController = building;
        Debug.Log("BuildingController set: " + buildingController.gameObject.name);
    }
}
