using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public GameObject Prota;
	public GameObject Car;
    public GameObject CarCam;
    public Vector3 characterPosition;
    public Vector3 carPosition;
    public bool inVehicle = false;
    public int cash = 0;
    // Start is called before the first frame update
    public void Save()
    {
        QuickSaveWriter.Create("Inputs")
                       .Write("Input1", Prota.transform.position)
                       .Write("Input2", Prota.transform.rotation)
					   .Write("Input3", Car.transform.position)
                       .Write("Input4", Car.transform.rotation)
					   .Write("Input5", CarCam.transform.position)
                       .Write("Input7", CarCam.transform.rotation)
                       .Write("Input6", inVehicle)
                       .Write("Cash", cash)
                       .Commit();
    }

    // Update is called once per frame
    public void Load()
    {
        QuickSaveReader.Create("Inputs")
                       .Read<Vector3>("Input1", (r) => {Prota.transform.position = r; })
                       .Read<Quaternion>("Input2", (r) => {Prota.transform.rotation = r; })
					   .Read<Vector3>("Input3", (r) => {Car.transform.position = r; })
                       .Read<Quaternion>("Input4", (r) => {Car.transform.rotation = r; })
					   .Read<Vector3>("Input5", (r) => {CarCam.transform.position = r; })
                       .Read<Quaternion>("Input7", (r) => {CarCam.transform.rotation = r; })
                       .Read<bool>("Input6", (r) => {inVehicle = r; });
                       
    }
    public void NewGame()
    {

    }
    public void ResetDefaultValues()
    {
        
    }
}
