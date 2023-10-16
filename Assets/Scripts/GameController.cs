using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform selectionAreaTransform;
    private Vector3 startMousePosition;
    private List<UnitController> selectedUnitList;
    public static GameController Instance;

    void Awake(){
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        selectedUnitList = new List<UnitController>();
        selectionAreaTransform.gameObject.SetActive(false);
    }

    void Update(){
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

    public void AddToSelectedUnits(UnitController unit){
        selectedUnitList.Add(unit);
    }

    public void RemoveFromSelectedUnits(UnitController unit){
        selectedUnitList.Remove(unit);
    }

    public void DeselectAllUnits(){
        foreach(UnitController unit in selectedUnitList){
            unit.SetSelectedVisible(false);
        }
        selectedUnitList.Clear();
    }
    
    void SelectUnitsInSelectionBox(Vector3 currentMousePosition){
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startMousePosition, currentMousePosition);

        foreach(Collider2D collider in collider2DArray) {
            UnitController entity = collider.GetComponent<UnitController>();
            if(entity != null){
                entity.SetSelectedVisible(true);
                selectedUnitList.Add(entity);
            }
        }
    }

    void HandleLeftMouseDown(Vector3 currentMousePosition){
        Collider2D collider = Physics2D.OverlapPoint(currentMousePosition);
        if(collider != null) {
            UnitController entity = collider.GetComponent<UnitController>();
            if(entity != null){
                if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)){
                    DeselectAllUnits();
                }
                entity.SetSelectedVisible(true);
                AddToSelectedUnits(entity);
            } else {
                StartSelectionBox(currentMousePosition);
            }
        }else {
            StartSelectionBox(currentMousePosition);
        }
    }

    void HandleLeftMouseUp(Vector3 currentMousePosition){
        selectionAreaTransform.gameObject.SetActive(false);

        if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)){
            DeselectAllUnits();
        }

        SelectUnitsInSelectionBox(currentMousePosition);
    }

    void HandleRightMouseDown(Vector3 currentMousePosition){
        DeselectAllUnits();

        Vector3 movePosition = currentMousePosition;
        List<Vector3> targetPositionList = GetPositionListAround(movePosition, new float[] {10f, 20f, 30f}, new int[] { 5, 10, 20});
        
        MoveSelectedUnitsToTargetPositions(targetPositionList);
    }

    void HandleRightMouseUp(Vector3 currentMousePosition){
        Collider2D collider = Physics2D.OverlapPoint(currentMousePosition);
        if(collider != null) {
            UnitController entity = collider.GetComponent<UnitController>();
            if(entity != null && selectedUnitList.Contains(entity)){
                entity.SetSelectedVisible(false);
                selectedUnitList.Remove(entity);
            } else {
                DeselectAllUnits();
                Vector3 movePosition = currentMousePosition;
                List<Vector3> targetPositionList = GetPositionListAround(movePosition, new float[] {10f, 20f, 30f}, new int[] { 5, 10, 20});
                MoveSelectedUnitsToTargetPositions(targetPositionList);
            }
        }
    }

    void MoveSelectedUnitsToTargetPositions(List<Vector3> targetPositionList){
        int targetPositionListIndex = 0;

        foreach(UnitController unit in selectedUnitList){
            //unit.MoveTo(targetPositionList[targetPositionListIndex]);
            targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
        }
    }

    private List<Vector3> GetPositionListAround(Vector3 startMousePosition, float[] ringDistanceArray, int[] ringPositionCountArray){
        int i;
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startMousePosition);
        for(i = 0; i < ringDistanceArray.Length; i++){
            positionList.AddRange(GetPositionListAround(startMousePosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }

    private List<Vector3> GetPositionListAround(Vector3 startMousePosition, float distance, int positionCount){
        int i;
        List<Vector3> positionList = new List<Vector3>();
        for(i = 0; i < positionCount; i++){
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
}