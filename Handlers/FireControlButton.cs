using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControlButton : MonoBehaviour {

    public static FireControlButton Instance { private set; get; }

    public EventHandler onFireButtonPressed;

    private void Awake() {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onFireButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ActivatedFireButton() {
        onFireButtonPressed?.Invoke(this, EventArgs.Empty);
    }


}
