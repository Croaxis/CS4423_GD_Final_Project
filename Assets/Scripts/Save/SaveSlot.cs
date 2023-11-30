using UnityEngine;
using UnityEngine.UI;

namespace cs4423fp.Save
{
    public class SaveSlot : MonoBehaviour
    {
        public Text saveNameText;
        public Button loadButton;
        public string saveFileName;

        public void SetSaveData(string saveName)
        {
            saveNameText.text = saveName;
            saveFileName = saveName;
        }

        public void LoadSave()
        {
            SaveSystem.LoadGame(saveFileName);
        }
    }
}

