using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Image UIlifeBar;
    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        this.UIlifeBar = GetComponent<Image>();

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        UIlifeBar.fillAmount = healthSystem.GetHealthPercent();

        if(healthSystem.GetHealthPercent() < 0.6f && healthSystem.GetHealthPercent() > 0.4f)
        {
            UIlifeBar.color = new Color(255f, 255f, 255f);
        }
        else if(healthSystem.GetHealthPercent() < 0.4f)
        {
            UIlifeBar.color = new Color(255f, 0f, 255f);
        }
    }

}
