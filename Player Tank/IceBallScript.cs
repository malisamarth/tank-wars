using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallScript : MonoBehaviour
{
    public static IceBallScript Instance { get; private set; }

    [SerializeField] private Rigidbody2D BulletRigidbody2D;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        BulletRigidbody2D.velocity = new Vector3(0f, 7f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
