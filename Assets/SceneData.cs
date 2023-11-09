using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public bool sceneEnabled = true;
    // Start is called before the first frame update
    public void SaveSceneState()
    {
        QuickSaveWriter.Create("Inputs")
                       .Write("Scene1", sceneEnabled)
                       .Commit();
    }

    // Update is called once per frame
    public void LoadSceneState()
    {
        QuickSaveReader.Create("Inputs")
                       .Read<bool>("Scene1", (r) => {sceneEnabled = r; });
    }
    public void SaveTempSceneState()
    {
        QuickSaveWriter.Create("Temp")
                       .Write("Scene1", sceneEnabled)
                       .Commit();
    }

    // Update is called once per frame
    public void LoadTempSceneState()
    {
        QuickSaveReader.Create("Temp")
                       .Read<bool>("Scene1", (r) => {sceneEnabled = r; });
    }

    public void SetDefault(){
        QuickSaveWriter.Create("Inputs")
                       .Write("Scene1", true)
                       .Commit();
    }
}
