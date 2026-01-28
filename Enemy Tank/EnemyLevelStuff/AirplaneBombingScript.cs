using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneBombingScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private float bombInterval = 1f;

    private float timer;


    [SerializeField] private List<ParticleSystem> particleSystemsArray = new List<ParticleSystem>(); 

    private void Update()
    {
        if (timer < Time.deltaTime) 
        { 
            timer += Time.deltaTime; 
        } else 
        { 
            timer = 0;
            DropBomb(); 
        }

        //Debug.Log(particleSystemsArray.Count);

        //DestroyTheBombs();

    }

    private void DropBomb()
    {
        ParticleSystem spawnedExplosion =
            Instantiate(explosion, transform.position, Quaternion.identity);

        spawnedExplosion.Play();

        //particleSystemsArray.Add(spawnedExplosion);
        //StartCoroutine(DestroyExplosion(spawnedExplosion));
    }

    IEnumerator DestroyExplosion(ParticleSystem explosionInstance)
    {
        yield return new WaitForSeconds(2f);
        Destroy(explosionInstance.gameObject);
    }
    
    private void DestroyTheBombs()
    {
        if (particleSystemsArray.Count >= 8)
        {
            for (int index = 0; index <= particleSystemsArray.Count; index++)
            {
                Destroy(particleSystemsArray[index]);
            }
        }

    }


}
