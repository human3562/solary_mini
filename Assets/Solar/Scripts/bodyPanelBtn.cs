using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class bodyPanelBtn : MonoBehaviour
{
    private Button btn;
    public Button infoBtn;
    private BodyManager bodyManager;
    public CelestialBody attachedBody;
    private void Start()
    {
        btn = GetComponent<Button>();
        bodyManager = GameObject.Find("Physics Manager").GetComponent<BodyManager>();
        btn.onClick.AddListener(() => { Camera.main.GetComponent<solaryCamera>().setTargetBody(attachedBody); });
        infoBtn.onClick.AddListener(()=> { bodyManager.setSelectedBody(attachedBody); });
    }
}
