using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class EnterVehicle : MonoBehaviour
{
    private bool inVehicle = false;
  
    public GameObject car_guiObj;
    public GameObject player_gui;
    public GameObject enterbutton;
    public GameObject exitbutton;
    public GameObject player;
    public GameObject car;
    public GameObject car_cam;
    public GameObject mirror;
    public GameObject  [] clan_members;
    public GameObject playerMinimap;
    public GameObject  carMinimap;
    public string eButton = "e"; 

    private void Awake()
    {
      
        car_cam.SetActive(false);
       
    }

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("car");
        //car_guiObj = GameObject.FindGameObjectWithTag("carpanel");
        //player_gui = GameObject.FindGameObjectWithTag("playerpanel");
        //enterbutton = GameObject.FindGameObjectWithTag("carenterbutton");
        //exitbutton = GameObject.FindGameObjectWithTag("carexitbutton");
        //car.GetComponent<Rigidbody>().isKinematic = true;
        //car.GetComponent<CarAudio>().enabled = false;
        //car.GetComponent<CarController>().enabled = false;

        //car.GetComponent<CarUserControl>().enabled = false;
        //player = GameObject.FindWithTag("Player");
       
        car_guiObj.SetActive(false);
        enterbutton.SetActive(false);
        exitbutton.SetActive(false);
        player_gui.SetActive(true);
        mirror.SetActive(false);
        car.GetComponent<CarControllerPlayer>().enabled = false;
        //car_cam.SetActive(false);
    }
    
    void Update()
    {
        // Verifica si se ha presionado la tecla "e"
        if (Input.GetKeyDown(eButton))
        {
            // Si el jugador está dentro del carro, sale; de lo contrario, entra en el carro
            if (inVehicle)
            {
                exit_from_car();
            }
            else
            {
                // Verifica si el jugador está cerca del carro antes de entrar
                float distanceToCar = Vector3.Distance(player.transform.position, car.transform.position);
                if (distanceToCar < 5.0f) // Cambia este valor según la distancia adecuada
                {
                    enter_in_car();
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Prota")
        {
            print("triggered");

                enterbutton.SetActive(true);
        }
    }
    private IEnumerator timelineoff()
    {
        yield return new WaitForSeconds(2f);
       

    }
    public void enter_in_car()
    {
        //clan_members = GameObject.FindGameObjectsWithTag("clan");
        //car.GetComponent<CarAudio>().enabled = true;
        //car.GetComponent<CarController>().enabled = true;
        //car.GetComponent<CarUserControl>().enabled = true;
        print("entered");
        car_guiObj.SetActive(true);
        mirror.SetActive(true);
        player_gui.SetActive(false);
        enterbutton.SetActive(false);
        exitbutton.SetActive(true);
        player.transform.parent = car.gameObject.transform;
        
        car_cam.SetActive(true);
        player.SetActive(false);
        playerMinimap.SetActive(false);
        carMinimap.SetActive(true);
        inVehicle = true;
        timelineoff();
        car.GetComponent<CarControllerPlayer>().enabled = true;

        car.GetComponent<Rigidbody>().isKinematic = false;
       /// foreach (GameObject claan in clan_members)
      //  {
        //    claan.transform.parent = car.gameObject.transform;
       //     claan.SetActive(false);
      //  }
    }
    public void exit_from_car()
    {
        if (inVehicle == true )
        {
            //car.GetComponent<Rigidbody>().isKinematic = true;
            //car.GetComponent<CarAudio>().enabled = false;
            //car.GetComponent<CarController>().enabled = false;
            //car.GetComponent<CarUserControl>().enabled = false;
            player.SetActive(true);
            player.transform.parent = null;
            car.GetComponent<Rigidbody>().isKinematic = true;
            car.GetComponent<CarControllerPlayer>().enabled = false;
            car_guiObj.SetActive(false);
            player_gui.SetActive(true);
            car_cam.SetActive(false);
            exitbutton.SetActive(false);
            mirror.SetActive(false);
            inVehicle = false;
            playerMinimap.SetActive(true);
            carMinimap.SetActive(false);
            print("exit car");
            //foreach (GameObject claan in clan_members)
            //{
              //  claan.transform.parent = null;
              //  claan.SetActive(true);
           // }

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Prota")
        {
            print("triggered");
            enterbutton.SetActive(false);
        }
    }
  
}