using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace cs4423fp.Load
{
    public class LoadMenu : MonoBehaviour
    {
        public Transform contentTransform; // Reference to the Content object in the Scroll View
        public GameObject saveSlotPrefab;

        private void Start()
        {
            PopulateSaveSlots();
        }

        private void PopulateSaveSlots()
        {
            // Clear existing save slots
            foreach (Transform child in contentTransform)
            {
                Destroy(child.gameObject);
            }

            // Get a list of save files (you may need to customize this based on your save file structure)
            //string[] saveFiles = SaveSystem.GetSaveFiles();

            // Instantiate save slots
            // foreach (string saveFile in saveFiles)
            // {
            //     GameObject saveSlot = Instantiate(saveSlotPrefab, contentTransform);
            //     //SaveSlot saveSlotScript = saveSlot.GetComponent<SaveSlot>();
            //     //saveSlotScript.SetSaveData(saveFile);
            // }
        }
    }   
}