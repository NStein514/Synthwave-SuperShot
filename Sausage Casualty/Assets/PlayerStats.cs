using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;

    public Healthbar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetSliderMax(maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0) SceneManager.LoadScene("UI test");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(damage);
        healthBar.SetSlider(currentHealth);
    }

}
