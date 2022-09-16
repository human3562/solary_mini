using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;
    private BodyManager bodyManager;

    private void Start()
    {
        bodyManager = GameObject.Find("Physics Manager").GetComponent<BodyManager>();
        slider.onValueChanged.AddListener((v)=>
        {
            sliderText.text = "= " + v.ToString("0.00") + " дней в секунду";
            bodyManager.timeMultiplier = v;
        });
    }
}
