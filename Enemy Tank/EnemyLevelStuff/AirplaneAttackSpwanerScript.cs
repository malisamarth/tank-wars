using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneAttackSpwanerScript : MonoBehaviour
{

    [SerializeField] private GameObject AirplaneAttackPrefab;

    [SerializeField] bool AirplaneAttackEnabled = true;

    [SerializeField] private float coolDownTime = 4f;
    private float timer = 0f;

    private AirplaneAttackScript airplaneAttackScript;

    private void Update()
    {

        if (timer >= coolDownTime)
        {
            timer = 0f;
            SingleAirplane();
        } else
        {
            timer += Time.deltaTime;
            AirplaneAttackEnabled = true;
        }
    }


    private void SingleAirplane()
    {
        if (AirplaneAttackEnabled)
        {
            Spwan();
            AirplaneAttackEnabled = false;
        }
    }

    private void Spwan()
    {
        Instantiate(AirplaneAttackPrefab);
        airplaneAttackScript = AirplaneAttackPrefab.GetComponent<AirplaneAttackScript>();
        //Debug.Log($"Incoming attack : {airplaneAttackScript.GetAirAttackType()}");
    }

}
