using Unity.VisualScripting;
using UnityEngine;

public class PropellerSpiningScript : MonoBehaviour
{
    private float rotationSpeed = 360f;
    [SerializeField] private float speedRate = 2f;

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * speedRate);
    }


}
