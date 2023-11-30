using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cs4423fp.Units.Player;

namespace cs4423fp.InputController
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance;

        private RaycastHit2D hit;

        public List<Transform> selectedUnits = new List<Transform>();
        public Transform selectedStructure = null;

        public LayerMask interactableLayer = new LayerMask();

        private bool isDragging = false;

        private Vector2 mousePosition;

        private void Awake(){
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
        }
         
        private void OnGUI(){
            if (isDragging){
                Rect rect = MultiSelect.GetScreenRect(mousePosition, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0f, 1f, 0f, 0.25f));
                MultiSelect.DrawScreenRectBorder(rect, 3, Color.green);
            }
        }

        public void HandleUnitMovement(){
            
            if(Input.GetMouseButtonDown(0)){
                if (EventSystem.current.IsPointerOverGameObject()){ return; }
                mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.Raycast(ray.origin, ray.direction, 100, interactableLayer);

                if(hit.collider != null){
                    if(addUnit(hit.transform, (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))){

                    }
                    else if(addStructure(hit.transform)){

                    }
                    else
                    {
                    }
                }
                else{

                    isDragging = true;
                    deselectUnits();
                }
                
            }
            if( Input.GetMouseButtonUp(0)){
                foreach(Transform child in Player.PlayerController.Instance.PlayerUnits){
                    foreach (Transform unit in child){
                        if (isInSelectionBox(unit)){
                            addUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }

            if(Input.GetMouseButtonDown(1) && haveUnitsSelected()){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.Raycast(ray.origin, ray.direction);

                if(hit.collider != null){
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 6:
                            break;
                        case 7:
                            foreach(Transform unit in selectedUnits){
                                PlayerUnit pUnit = unit.gameObject.GetComponent<PlayerUnit>();
                                //pUnit.Attack(hit.point);
                            }
                            break;
                        default:
                            foreach(Transform unit in selectedUnits){
                                PlayerUnit pUnit = unit.gameObject.GetComponent<PlayerUnit>();
                                pUnit.MoveUnit(hit.point);
                            }
                            break;
                    }
                }
            }
            else if(Input.GetMouseButtonDown(1) && selectedStructure != null){
                selectedStructure.gameObject.GetComponent<Interactables.IStructure>().SetSpawnMarkerLocation();
            }
        }

        private void deselectUnits(){
            if (selectedStructure != null){
                selectedStructure.gameObject.GetComponent<Interactables.IStructure>().OnInteractExit();
                selectedStructure = null;
            }

            if(selectedUnits.Count > 0){
                for (int i = 0; i < selectedUnits.Count; i++){
                    Transform unitTransform = selectedUnits[i];
                    if (unitTransform != null)
                    {
                        Interactables.IUnit unit = unitTransform.GetComponent<Interactables.IUnit>();
                        if (unit != null)
                        {
                            unit.OnInteractExit();
                        }
                    }
                }
                selectedUnits.Clear();
            }
        }

        private bool isInSelectionBox(Transform tf) // Still Broken
        {
            if (!isDragging){
                return false;
            }

            Camera cam = Camera.main;
            Bounds vpBounds = MultiSelect.GetViewportBounds(cam, mousePosition, Input.mousePosition);
            Vector2 viewportPoint = cam.WorldToViewportPoint(tf.position);

            return vpBounds.Contains(viewportPoint);
        }


        private bool haveUnitsSelected(){
            return selectedUnits.Count > 0;
        }

        private Interactables.IUnit addUnit(Transform tf, bool canMultiselect = false){
            Interactables.IUnit iUnit = tf.GetComponent<Interactables.IUnit>();
            if (iUnit){
                if (!canMultiselect){
                    deselectUnits();
                }
                selectedUnits.Add(iUnit.gameObject.transform);
                iUnit.OnInteractEnter();

                return iUnit;
            }
            else{
                return null;
            }
        }

        private Interactables.IStructure addStructure(Transform tf){
            Interactables.IStructure iStructure = tf.GetComponent<Interactables.IStructure>();
            if (iStructure){
                deselectUnits();
                selectedStructure = iStructure.gameObject.transform;
                iStructure.OnInteractEnter();

                return iStructure;
            }
            else{
                return null;
            }
        }
    }
}
