using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour
{
    public enum ImageType {rotation, filled};
    public ImageType imgType;

    public Gradient colorGradient;

    [SerializeField] private float lowGaugeValue;
    [SerializeField] private float highGaugeValue;
    [SerializeField] private float topValue;
    [SerializeField] private int direction = -1;
    [SerializeField] private bool setValueOnAwake = false;

    private Vector3 currentRotation;
    private Image fillImage;
    // Start is called before the first frame update
    void Start()
    {
        fillImage = GetComponent<Image>();
        currentRotation = GetComponent<RectTransform>().eulerAngles;
        if(setValueOnAwake) SetValues();
        
    }

    void SetValues(){
        ApplyCalculation(1000);
    }

    // Update is called once per frame
    void SliderTest(float slideValue)
    {
        ApplyCalculation(slideValue);
    }

    public void ApplyCalculation(float actual){
        float v1 = actual/topValue;

        switch(imgType){
            case ImageType.rotation:
                float v2 = highGaugeValue - lowGaugeValue;
                float v3 = v1 * v2 + lowGaugeValue;
                ApplyRotation(v3 * direction);
                break;
            case ImageType.filled:
                ApplyFill(v1);
                break;
        }
    }

    void ApplyRotation(float rotate){
        Vector3 setRotate = new Vector3(0,0,rotate);
        transform.eulerAngles = setRotate;
    }

    private void ApplyFill(float fill)
    {
        
            fillImage.fillAmount = fill;
            fillImage.color = colorGradient.Evaluate(fill);
        
        
    }
}
