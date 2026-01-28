using System;
using Unity.VisualScripting;
using UnityEngine;

public class MiniWarMachineScript : MonoBehaviour {

    public static MiniWarMachineScript Instance { get; private set; }

    [Header("References")]
    [SerializeField] private PlayerTankMove player;        // PLAYER reference
    [SerializeField] private Transform laserMuzzle;   // Laser start point
    [SerializeField] private LineRenderer line;

    [Header("Laser Settings")]
    /*[SerializeField]*/ private float laserDistance;

    [SerializeField] private bool allowFire = true;

    private MiniMachineHealthBar miniMachineHealthBar;

    public event EventHandler OnLaserStartShooting;
    public event EventHandler OnLaserStopsShooting;

    private void Awake() {
        Instance = this;

        line.positionCount = 2;
        line.enabled = true;
    }

    private void Start() {
        player = PlayerTankMove.Instance.GetComponent<PlayerTankMove>();
        miniMachineHealthBar = MiniMachineHealthBar.Instacnce.GetComponent<MiniMachineHealthBar>();
        miniMachineHealthBar.OnLaserShootingTimeActivated += MiniMachineHealthBar_OnLaserShootingTimeActivated;
        miniMachineHealthBar.OnLaserShootingTimeDeactivated += MiniMachineHealthBar_OnLaserShootingTimeDeactivated;

    }

    private void MiniMachineHealthBar_OnLaserShootingTimeDeactivated(object sender, System.EventArgs e) {

        if (!allowFire) {
            SetAllowFire();
        }

        
    }

    private void MiniMachineHealthBar_OnLaserShootingTimeActivated(object sender, System.EventArgs e) {

        if (allowFire) {
            ClearLaserAtPlayer();
        }
    }

    private void Update() {

        if (allowFire) {
            GetDistance();
            FireLaserAtPlayer();

        } else {
            ClearLaser();
        }

    }

    private void FireLaserAtPlayer() {
        if (player == null) return;

        OnLaserStartShooting?.Invoke(this, EventArgs.Empty);

        line.positionCount = 2;

        Vector3 start = laserMuzzle.position;

        // Direction FROM laser TO player
        Vector3 direction = (player.transform.position - start).normalized;

        Vector3 end = start + direction * laserDistance;

        line.SetPosition(0, start);
        line.SetPosition(1, end);

        //line.SetWidth(0.2f, 0.2f);

        //ColorGra = line.colorGradient;

        // Debug (Scene view)
        Debug.DrawRay(start, direction * laserDistance, Color.red);

    }


    private void GetDistance() {
        float yDistance = Mathf.Abs(laserMuzzle.position.y - player.transform.position.y);
        laserDistance = yDistance;
    }

    private void ClearLaser() {

        OnLaserStopsShooting?.Invoke(this, EventArgs.Empty);

        line.positionCount = 0;
    }

    private void SetAllowFire() {
        allowFire = true;
    }

    private void ClearLaserAtPlayer() {
        allowFire = false;
    }

}






