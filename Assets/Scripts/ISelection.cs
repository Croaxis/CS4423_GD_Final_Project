using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelection
{
    void SetSelectedVisible(bool visible);
    bool IsSelected();
    GameObject GetGameObject();
}

