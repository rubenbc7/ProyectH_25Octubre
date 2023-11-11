using CI.QuickSave;
using UnityEngine;
using UnityEngine.SceneManagement;


// This class is created for the example scene. There is no support for this script.
public class ControlsTutorial : MonoBehaviour
{
	public bool lockedMouse;
	private string message = "";
	private bool showMsg = false;

	private int w = 550;
	private int h = 100;
	private Rect textArea;
	private GUIStyle style;
	private Color textColor;

	private GameObject keyboardCommands;
	private GameObject gamepadCommands;
	public bool mouselocked;
	public GameObject pauseMenu;
	private bool ispaused;
	public Vector3 characterPosition;
	public PlayerData playerData;
	public SceneData sceneData;
	public Vector3 carPosition;

	private void Awake()
	{
		//Load();
		ispaused = false;
		lockedMouse = true;
		style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 36;
		style.wordWrap = true;
		textColor = Color.white;
		textColor.a = 0;
		textArea = new Rect((Screen.width-w)/2.0f, 0, w, h);

		keyboardCommands = this.transform.Find("ScreenHUD/Keyboard").gameObject;
		gamepadCommands = this.transform.Find("ScreenHUD/Gamepad").gameObject;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || mouselocked == false)//Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		if(Input.GetKeyDown("escape"))
		{
			PauseMenu();
		}
		if ( mouselocked == true)
		{
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
		}
		
		keyboardCommands.SetActive(Input.GetKey(KeyCode.F2));
		gamepadCommands.SetActive(Input.GetKey(KeyCode.F3) || Input.GetKey(KeyCode.Joystick1Button7));
	}
	public void PauseMenu(){
		if(!ispaused){

		
			pauseMenu.SetActive(true);
			mouselocked = true;
			ispaused = true;
			Time.timeScale = 0f;

			return;
		}
		if(ispaused)
		{
			pauseMenu.SetActive(false);
			mouselocked = false;
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
		SceneManager.LoadSceneAsync("PersistentGameplay", LoadSceneMode.Additive);
		Time.timeScale = 1f;
	}

	void OnGUI()
	{
		if(showMsg)
		{
			if(textColor.a <= 1)
				textColor.a += 0.5f * Time.deltaTime;
		}
		// no hint to show
		else
		{
			if(textColor.a > 0)
				textColor.a -= 0.5f * Time.deltaTime;
		}

		style.normal.textColor = textColor;

		GUI.Label(textArea, message, style);
	}

	public void SetShowMsg(bool show)
	{
		showMsg = show;
	}
	

	public void SetMessage(string msg)
	{
		message = msg;
	}
}
