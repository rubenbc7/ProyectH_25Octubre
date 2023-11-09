using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbleEnable : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    // Start is called before the first frame update

    void Able()
    {
        _object.SetActive(true);
    }

    // Update is called once per frame
    void Enable()
    {
        _object.SetActive(false);
    }
}
