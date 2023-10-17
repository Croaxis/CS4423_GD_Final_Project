using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform selectionAreaTransform;
    [SerializeField] float spacing = 1.5f;
    private Vector3 startMousePosition;
    private Dictionary<ISelection, GameObject> selectionToGameObjectMap = new Dictionary<ISelection, GameObject>();
    private List<ISelection> selectedUnitList = new List<ISelection>();
    public static GameController Instance;

    public event Action<List<UnitController>> OnSelectedUnitsChanged;
    public event Action<List<ISelection>> OnSelectedListChanged;

    void Awake(){
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        selectedUnitList = new List<ISelection>();
        selectedUnitList.Clear();
        selectionAreaTransform.gameObject.SetActive(false);
    }

    void Update(){
        if (Time.timeScale == 0f){return;}
        
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0)){
            HandleLeftMouseDown(currentMousePosition);
        }

        if(Input.GetMouseButton(0)){
            UpdateSelectionBox(currentMousePosition);
        }

        if(Input.GetMouseButtonUp(0)){
            HandleLeftMouseUp(currentMousePosition);
        }

        if(Input.GetMouseButtonDown(1)){
            Collider2D collider = Physics2D.OverlapPoint(currentMousePosition);
            if(collider != null) {
                UnitController entity = collider.GetComponent<UnitController>();
                if(entity != null && selectedUnitList.Contains(entity)){
                    HandleRightMouseUp(currentMousePosition);
                } else {
                    HandleRightMouseDown(currentMousePosition);
                }
            } else {
                HandleRightMouseDown(currentMousePosition);
            }
        }
    }

    void StartSelectionBox(Vector3 currentMousePosition){
        selectionAreaTransform.gameObject.SetActive(true);
        startMousePosition = currentMousePosition;
    }

    void UpdateSelectionBox(Vector3 currentMousePosition){
        Vector3 lowerLeft = new Vector3(Mathf.Min(startMousePosition.x, currentMousePosition.x), Mathf.Min(startMousePosition.y, currentMousePosition.y));
        Vector3 upperRight = new Vector3(Mathf.Max(startMousePosition.x, currentMousePosition.x), Mathf.Max(startMousePosition.y, currentMousePosition.y));
        selectionAreaTransform.position = lowerLeft;
        selectionAreaTransform.localScale = upperRight - lowerLeft;
    }

    private GameObject GetGameObjectForSelection(ISelection selection)
    {
        // If the mapping already exists, return the associated GameObject
        if (selectionToGameObjectMap.TryGetValue(selection, out GameObject gameObject))
        {
            return gameObject;
        }
        else
        {
            GameObject newGameObject = selection.GetGameObject();
            if (newGameObject != null)
            {
                selectionToGameObjectMap.Add(selection, newGameObject); // Add mapping to the dictionary
                return newGameObject;
            }
            return null;
        }
    }

    public void AddToSelectedEntities(ISelection entity)
    {
        GameObject entityGameObject = GetGameObjectForSelection(entity);
        if (entityGameObject != null)
        {
            selectedUnitList.Add(entity);
            OnSelectedListChanged?.Invoke(selectedUnitList);
            DebugSelectedList(selectedUnitList);
        }
    }

    public void RemoveFromSelectedEntities(ISelection entity)
    {
        GameObject entityGameObject = GetGameObjectForSelection(entity);
        if (entityGameObject != null)
        {
            selectedUnitList.Remove(entity);
            selectionToGameObjectMap.Remove(entity); // Remove mapping from the dictionary
            OnSelectedListChanged?.Invoke(selectedUnitList);
            DebugSelectedList(selectedUnitList);
        }
    }

    public void DeselectAllEntities(){
        foreach(UnitController unit in selectedUnitList.OfType<UnitController>()){
            unit.SetSelectedVisible(false);
        }
        selectedUnitList.Clear();
    }
    
    void SelectUnitsInSelectionBox(Vector3 currentMousePosition)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startMousePosition, currentMousePosition);

        foreach (Collider2D collider in collider2DArray)
        {
            Debug.Log("Collider detected: " + collider.gameObject.name);
            UnitController unitController = collider.GetComponent<UnitController>();
            BuildingController buildingController = collider.GetComponent<BuildingController>();

            if (unitController != null && collider.CompareTag("Player") && !selectedUnitList.Contains(unitController))
            {
                unitController.SetSelectedVisible(true);
                selectedUnitList.Add(unitController);
            }
            else if (buildingController != null && collider.CompareTag("Player") && !selectedUnitList.Contains(buildingController))
            {
                buildingController.SetSelectedVisible(true);
                selectedUnitList.Add(buildingController);
            }
        }
    }

    void HandleLeftMouseDown(Vector3 currentMousePosition)
    {
        Collider2D collider = Physics2D.OverlapPoint(currentMousePosition);

        if (collider != null)
        {
            BuildingController building = collider.GetComponent<BuildingController>();
            UnitController unit = collider.GetComponent<UnitController>();

            if (building != null && building.CompareTag("Player"))
            {
                // Deselect all entities if Shift key is not pressed
                if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                {
                    DeselectAllEntities();
                }

                // Toggle building selection
                if (selectedUnitList.Contains(building))
                {
                    building.SetSelectedVisible(false);
                    RemoveFromSelectedEntities(building);
                }
                else
                {
                    building.SetSelectedVisible(true);
                    AddToSelectedEntities(building);
                }
            }
            else if (unit != null && unit.CompareTag("Player"))
            {
                // Deselect all entities if Shift key is not pressed
                if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                {
                    DeselectAllEntities();
                }

                // Toggle unit selection
                unit.SetSelectedVisible(!unit.IsSelected());

                if (unit.IsSelected())
                {
                    AddToSelectedEntities(unit);
                }
                else
                {
                    RemoveFromSelectedEntities(unit);
                }
            }
            else
            {
                // Deselect building if clicking on the ground
                if (selectedUnitList.Count > 0 && selectedUnitList[0] is BuildingController)
                {
                    DeselectAllEntities();
                }
                // Start selection box if clicking on the ground without selecting any entity
                else
                {
                    StartSelectionBox(currentMousePosition);
                }
            }
        }
        else
        {
            // Start selection box if clicking on the ground without any colliders
            StartSelectionBox(currentMousePosition);
        }
    }

    void HandleLeftMouseUp(Vector3 currentMousePosition){
        selectionAreaTransform.gameObject.SetActive(false);

        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            DeselectAllEntities();
        }

        SelectUnitsInSelectionBox(currentMousePosition);

        // If any building is part of the selection, deselect it
        foreach (ISelection selectedEntity in selectedUnitList)
        {
            if (selectedEntity is BuildingController building && building.IsSelected())
            {
                building.SetSelectedVisible(false);
                RemoveFromSelectedEntities(building);
            }
        }

        // Notify listeners about the selected units change
        OnSelectedUnitsChanged?.Invoke(GetSelectedUnitList());
    }


    void HandleRightMouseDown(Vector3 currentMousePosition){
        if (selectedUnitList.Count > 0) {
            Vector3 movePosition = currentMousePosition;
            List<Vector3> targetPositionList = GetPositionListAround(movePosition, selectedUnitList.Count, spacing);
            
            MoveSelectedUnitsToTargetPositions(targetPositionList);
        }
    }

    void HandleRightMouseUp(Vector3 currentMousePosition){
        if (selectedUnitList.Count > 0 && selectedUnitList[0] is BuildingController)
        {
            DeselectAllEntities();
        }
        else if (selectedUnitList.Count > 0) {
            Collider2D collider = Physics2D.OverlapPoint(currentMousePosition);
            if(collider != null) {
                UnitController entity = collider.GetComponent<UnitController>();
                if(entity != null && selectedUnitList.Contains(entity)){
                    entity.SetSelectedVisible(false);
                    selectedUnitList.Remove(entity);
                } else {
                    DeselectAllEntities();
                    Vector3 movePosition = currentMousePosition;
                    List<Vector3> targetPositionList = GetPositionListAround(movePosition, selectedUnitList.Count, 1.5f);
                    MoveSelectedUnitsToTargetPositions(targetPositionList);
                }
            }
            OnSelectedUnitsChanged?.Invoke(GetSelectedUnitList());
        }
    }

    void MoveSelectedUnitsToTargetPositions(List<Vector3> targetPositionList)
    {
        int targetPositionListIndex = 0;
        List<UnitController> unitsToMove = new List<UnitController>();

        // Collect units from selectedUnitList
        foreach (ISelection selection in selectedUnitList)
        {
            if (selection is UnitController unitController)
            {
                unitsToMove.Add(unitController);
            }
        }

        // Move collected units to target positions
        foreach (UnitController unit in unitsToMove)
        {
            unit.MoveTo(targetPositionList[targetPositionListIndex]);
            targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
        }
    }

    private List<Vector3> GetPositionListAround(Vector3 startMousePosition, int unitCount, float spacing){
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startMousePosition);

        // Calculate the number of rings based on the unit count
        int ringCount = (int)Mathf.Ceil(Mathf.Sqrt(unitCount));

        // Calculate the distance and position count for each ring
        for(int i = 0; i < ringCount; i++){
            float distance = (i + 1) * spacing;
            int positionCount = Mathf.Min(unitCount - positionList.Count, (int)(2 * Mathf.PI * distance / spacing));
            positionList.AddRange(GetPositionListAroundRing(startMousePosition, distance, positionCount));
        }
        return positionList;
    }

    private List<Vector3> GetPositionListAroundRing(Vector3 startMousePosition, float distance, int positionCount){
        List<Vector3> positionList = new List<Vector3>();
        for(int i = 0; i < positionCount; i++){
            float angle = i * (360f / positionCount);
            Vector3 direction = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startMousePosition + direction * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vector, float angle){
        return Quaternion.Euler(0, 0, angle) * vector;
    }

    public List<UnitController> GetSelectedUnitList()
    {
        List<UnitController> selectedUnits = new List<UnitController>();
        foreach (ISelection selection in selectedUnitList)
        {
            if (selection is UnitController unitController)
            {
                selectedUnits.Add(unitController);
            }
        }
        return selectedUnits;
    }

    private void DebugSelectedList(List<ISelection> selectedEntities)
    {
        Debug.Log("Selected List:");
        foreach (ISelection selection in selectedEntities)
        {
            GameObject selectedGameObject = selection.GetGameObject();
            if (selectedGameObject != null)
            {
                Debug.Log(selectedGameObject.name);
            }
        }
    }
}