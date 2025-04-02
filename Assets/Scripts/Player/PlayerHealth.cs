using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;
    public float maxAlpha = 0.255f;

    private float durationTimer;
    public AudioManager audioManager;

    [Header("Stamina Bar")]
    public float maxStamina = 100f;
    private float stamina;
    public float staminaDrainSpeed = 20f; 
    public float staminaRegenSpeed = 10f;
    public Image staminaBar;
    public GameOverScreen gameOverScreen;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        health = maxHealth;
        stamina = maxStamina;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        UpdateHealthUI();
        UpdateStaminaUI();
        if (health <= 0)
        {
            Die();
            return;
        }
            
        if(overlay.color.a > 0)
        {
            if(health <= 30)
                return;
            durationTimer += Time.deltaTime;
            if(durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }

    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health < 0) health = 0f;
        lerpTimer = 0f;
        durationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, maxAlpha);
    }

    public void RestoreHealth(float healAmount)
    {
        audioManager.HealingSound();
        health += healAmount;
        if (health > maxHealth) health = maxHealth;
        lerpTimer = 0f;
    }

    public void UpdateStaminaUI()
    {
        staminaBar.fillAmount = stamina / maxStamina;
    }

    public void UseStamina(float amount)
    {
        stamina -= amount;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    public void RestoreStamina(float amount)
    {
        stamina += amount;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    public bool HasStamina(float amount)
    {
        return stamina >= amount;
    }

    public void Die()
    {
        Debug.Log("Player Died!");

        if (gameObject.GetComponent<MeshRenderer>() != null)
            gameObject.GetComponent<MeshRenderer>().enabled = false;

        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }

        if (GetComponent<PlayerMotor>() != null)
            GetComponent<PlayerMotor>().enabled = false;

        DisableLook();

        gameOverScreen.Setup();
    }

    public void DisableLook()
    {
        if (GetComponent<PlayerLook>() != null)
        {
            GetComponent<PlayerLook>().canLook = false;
        }
    }


}
