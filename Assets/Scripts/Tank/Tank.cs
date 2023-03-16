using System;
using UnityEngine;

public class Tank : MonoBehaviour, IShootable, IDamageReceiver, IPowerUpReceiver
{
    [SerializeField] private float defaultMoveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform turret;
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private Transform cannonEnd;
    [SerializeField] private GameObject tankNormal;
    [SerializeField] private GameObject tankDestroyed;

    private InputManager inputManager;
    private float moveSpeed;
    private int currentAmmunition;
    private int shieldAmount;
    private Rigidbody rigidBody;

    private const int MAX_AMMUNITION_AMOUNT = 15;
    private const int MAX_SHIELD_AMOUNT = 1;

    public event EventHandler OnTankDestroyed;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        moveSpeed = defaultMoveSpeed;
        currentAmmunition = 10;
        UIManager.Instance.UpdateAmmoUI(currentAmmunition);
        shieldAmount = 0;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;

        inputManager.OnCannonShot += InputManager_OnCannonShot;
    }

    private void OnDisable()
    {
        inputManager.OnCannonShot -= InputManager_OnCannonShot;
    }

    private void FixedUpdate()
    {
        GetTankMovement();
        GetTankRotation();
        TurnTurret();
    }

    private void InputManager_OnCannonShot(object sender, EventArgs e)
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.OnGoing)
        {
            if (currentAmmunition > 0)
            {
                Shoot();
                currentAmmunition--;
                UIManager.Instance.UpdateAmmoUI(currentAmmunition);
            }
        }
    }

    public void Shoot()
    {
        Instantiate(shellPrefab, cannonEnd.position, turret.rotation);
    }

    private void GetTankMovement()
    {
        Vector3 moveAmount = transform.forward * inputManager.GetMovementDirection();
        rigidBody.velocity = moveAmount * Time.deltaTime * moveSpeed;
    }

    private void GetTankRotation()
    {
        float rotation = inputManager.GetTankRotationDirection();
        rigidBody.MoveRotation(Quaternion.Euler(transform.rotation.eulerAngles + transform.up * rotation * Time.deltaTime * rotateSpeed));
    }

    private void TurnTurret()
    {
        turret.LookAt(inputManager.GetMouseWorldPosition());
        turret.eulerAngles = new Vector3(0, turret.eulerAngles.y, 0);
    }

    public void TakeDamage()
    {
        if(shieldAmount == 0)
        {
            inputManager.DisableControls();

            SwitchVisuals();
        }
        else
        {
            shieldAmount--;
        }
    }

    private void SwitchVisuals()
    {
        tankNormal.SetActive(false);
        tankDestroyed.SetActive(true);

        OnTankDestroyed.Invoke(this, EventArgs.Empty);
    }

    public void AddAmmo(int ammoAmount)
    {
        currentAmmunition += ammoAmount;
        currentAmmunition = Mathf.Clamp(currentAmmunition, 0, MAX_AMMUNITION_AMOUNT);
        UIManager.Instance.UpdateAmmoUI(currentAmmunition);
    }

    public void IncreaseSpeed(float speedIncreasePercentage, float activeTime)
    {
        ResetSpeed();
        moveSpeed *= (1 + speedIncreasePercentage);
        Invoke(nameof(ResetSpeed), 10);
    }

    private void ResetSpeed()
    {
        CancelInvoke(nameof(ResetSpeed));
        moveSpeed = defaultMoveSpeed;
    }

    public void AddShield()
    {
        shieldAmount += 1;
        shieldAmount = Mathf.Clamp(shieldAmount, 0, MAX_SHIELD_AMOUNT);
    }
}
