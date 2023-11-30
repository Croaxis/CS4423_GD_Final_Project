using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.UI.HUD
{
    public class Action : MonoBehaviour
    {
        public void OnClick(){
            ActionFrame.Instance.StartSpawn(name);
        }
    }
}