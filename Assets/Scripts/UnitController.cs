using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour, ISelection
{
    private IUnitData unitData;
    private GameObject selectedGameObject;
    private IMovePosition movePosition;
    private bool isSelected = false;
    
    private void Awake()
    {
        // Initialize unitData here
        unitData = GetComponent<IUnitData>();

        selectedGameObject = transform.Find("Selected").gameObject;
        movePosition = GetComponent<IMovePosition>();
        SetSelectedVisible(false);

        InspectorController inspectorController = FindObjectOfType<InspectorController>();
        if (inspectorController != null)
        {
            inspectorController.SetElementImage(unitData.GetUnitSprite());
        }

        // Set move speed in MoveVelocity component
        MoveVelocity moveVelocity = GetComponent<MoveVelocity>();
        if (moveVelocity != null)
        {
            moveVelocity.SetMoveSpeed(unitData.GetMoveSpeed());
        }
    }

    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    // Method to check if the unit is selected
    public bool IsSelected()
    {
        return isSelected;
    }

    // Method to set the selected state of the unit
    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        // Calculate the movement direction
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Set the move position
        movePosition.SetMovePosition(targetPosition);

        // Calculate the rotation angle based on the movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to face the movement direction
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public IUnitData GetUnitData()
    {
        return unitData;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}