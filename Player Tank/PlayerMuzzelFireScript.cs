using System.Collections;
using UnityEngine;

public class PlayerMuzzelFireScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzelFireEffect;

    private CannonBallSpwanScript cannonBallSpwanScript;

    private void Start()
    {
        cannonBallSpwanScript = CannonBallSpwanScript.Instance.GetComponent<CannonBallSpwanScript>();
        cannonBallSpwanScript.onCannonFired += CannonBallSpwanScript_onCannonFired;
    }

    private void CannonBallSpwanScript_onCannonFired(object sender, System.EventArgs e)
    {
        //muzzelFireEffect.Play();
        StartCoroutine(StopFlash());

        

    }

    IEnumerator StopFlash()
    {
        muzzelFireEffect.Play();
        Debug.Log("Muzzel Fire Effect Played");
        yield return new WaitForSeconds(0.25f);
        muzzelFireEffect.Stop();
    }

}
