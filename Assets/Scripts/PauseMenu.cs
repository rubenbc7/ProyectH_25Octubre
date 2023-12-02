using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool lockedMouse;
	
	public bool mouselocked;
	public GameObject pauseMenu;
	public bool ispaused;
	public Vector3 characterPosition;
	public PlayerData playerData;
	public SceneData sceneData;
	public Vector3 carPosition;

	private void Awake()
	{
		//Load();
		//ispaused = false;
		//lockedMouse = true;
		// Cursor.lockState = CursorLockMode.Locked;
		// Cursor.visible = false;
	}

	void Update()
	{
		//if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))//Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		//{
		//	Cursor.lockState = CursorLockMode.Locked;
		//	Cursor.visible = false;
		//}
		if(Input.GetKeyDown("escape"))
		{
			PauseMenuShow();
		}
		//if ( mouselocked == true)
		//{
			//Cursor.lockState = CursorLockMode.Confined;
			//Cursor.visible = true;
		//}
		
		//keyboardCommands.SetActive(Input.GetKey(KeyCode.F2));
		//gamepadCommands.SetActive(Input.GetKey(KeyCode.F3) || Input.GetKey(KeyCode.Joystick1Button7));
	}
	public void PauseMenuShow(){
		if(!ispaused){

		
			pauseMenu.SetActive(true);
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
			ispaused = true;
			Time.timeScale = 0f;

			return;
		}
		if(ispaused)
		{
			pauseMenu.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			ispaused = false;
			Time.timeScale = 1f;
			return;
		}
			
	}
	public void Resume(){
		pauseMenu.SetActive(false);
			mouselocked = false;
			ispaused = false;
			Time.timeScale = 1f;
	}
	public void Save(){
		
		//characterPosition = Prota.transform.position;
		
		playerData.Save();
		sceneData.SaveSceneState();
	}
	public void Load(){
		playerData.Load();
		sceneData.LoadSceneState();
		pauseMenu.SetActive(false);
			mouselocked = false;
			ispaused = false;
			Time.timeScale = 1f;
	}
	public void Exit(){
		SceneManager.LoadScene("MainMenu");
		SceneManager.LoadSceneAsync("StartCity", LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync("StartCityMountains", LoadSceneMode.Additive);
		Time.timeScale = 1f;
	}
}
