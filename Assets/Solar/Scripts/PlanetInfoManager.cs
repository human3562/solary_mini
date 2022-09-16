using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlanetInfoManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private TextMeshProUGUI distanceField;
    [SerializeField] private TextMeshProUGUI massField;
    [SerializeField] private TextMeshProUGUI speedField;
    [SerializeField] private Toggle SItoggle;

    private float _DistanceConversion = 6.68458712f * Mathf.Pow(10.0f, -12.0f); // m -> AU (Astronomical Units)
    private float _MassConversion = 1.67403241f * Mathf.Pow(10.0f, -25.0f); // kg -> E (Earth masses)
    private float _TimeConversion = 1.15740741f * Mathf.Pow(10.0f, -5.0f); // s -> D (Days)

    public bool SImeasures = false;

    private GameObject infoPanel;
    private void Start()
    {
        infoPanel = gameObject.transform.GetChild(0).gameObject;
        SItoggle.onValueChanged.AddListener((val)=> { SImeasures = val; });
    }

    public void hidePanel()
    {
        infoPanel.SetActive(false);
    }

    public void showPanel()
    {
        infoPanel.SetActive(true);
    }

    public void setNameField(string input)
    {
        nameField.text = input;
    }

    private string eToSuper(string input)
    {
        if (input.IndexOf('E') != -1)
        {
            string result = input.Replace("E", " * 10<sup>");
            result += "</sup>";
            return result;
        }
        else return input;
    }

    public void setDistanceField(float input)
    {
        if(SImeasures)
            distanceField.text = eToSuper(string.Format("{0:#.000E-0}", input / _DistanceConversion)) + " Ï.";
        else
            distanceField.text = eToSuper(string.Format("{0:#.000E-0}", input)) + " ‡.Â.";
    }

    public void setMassField(float input)
    {
        if (SImeasures)
            massField.text = eToSuper(string.Format("{0:#.000E-0}", input / _MassConversion)) + " Í„.";
        else
            massField.text = eToSuper(string.Format("{0:#.000E-0}", input)) + " Á.Ï.";
    }

    public void setSpeedField(float input)
    {
        if (SImeasures)
            speedField.text = eToSuper(string.Format("{0:#.000E-0}", input / (_DistanceConversion / _TimeConversion))) + " Ï/Ò.";
        else
            speedField.text = eToSuper(string.Format("{0:#.000E-0}", input)) + " ‡.Â/‰.";
    }


}
