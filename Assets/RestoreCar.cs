using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreCar : MonoBehaviour
{
    [SerializeField] private GameObject _playerCar;
    public CarControllerPlayer carControllerPlayer;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _carSpawnPoint;
    [SerializeField] private GameObject _normalCamera;
    [SerializeField] private GameObject _crashCamera;
    [SerializeField] private GameObject _crashUi;
    [SerializeField] private GameObject _raceUi;
    public Deform deform;
    public UIGauge uIGaugeNos;
    public UIGauge uIGaugeHealth;
    public EnterVehicle enterVehicle;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void RestoreCarFunction()
    {
        enterVehicle.exit_from_car();
        deform.crashed = false;
        _normalCamera.SetActive(true);
        _crashCamera.SetActive(false);
        _crashUi.SetActive(false);
        _raceUi.SetActive(true);
        carControllerPlayer = _playerCar.gameObject.GetComponent<CarControllerPlayer>();
        carControllerPlayer.CurrentNosLeft = carControllerPlayer.MaxNOSCapacity;
        deform.carHealth = 2000f;
        uIGaugeNos.ApplyCalculation(carControllerPlayer.CurrentNosLeft);
        uIGaugeHealth.ApplyCalculation(2000f);
        Debug.Log("Car restored");

        _player.transform.position = _spawnPoint.transform.position;
        _player.transform.rotation = _spawnPoint.transform.rotation;

        _playerCar.transform.position = _carSpawnPoint.transform.position;
        _playerCar.transform.rotation = _carSpawnPoint.transform.rotation;

        

        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }
}
