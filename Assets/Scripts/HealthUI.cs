using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Health;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        Health = GetComponent<TextMeshProUGUI>();
        Health.text = player.GetHealth().ToString();
    }
    public void DisplayHealth()
    {
        Health.text = player.GetHealth().ToString();
    }

  
}
