using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaxHitsButton : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Button button;
    [SerializeField] private TextMeshProUGUI textUI;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PressButton);
        playerMovement = GameObject.FindWithTag("Manager").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isMaxHits == true)
        {
            textUI.text = "Wall Hit Limit Activated";
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            playerMovement.isMaxHits = false;
            textUI.text = "Wall Hit Limit Deactivated";
            button.GetComponent<Image>().color = Color.white;
        }
    }
    public void PressButton()
    {
        if (playerMovement.isMaxHits == false)
        {
            playerMovement.isMaxHits = true;
            textUI.text = "Wall Hit Limit Activated";
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            playerMovement.isMaxHits = false;
            textUI.text = "Wall Hit Limit Deactivated";
            button.GetComponent<Image>().color = Color.white;
        }
    }
}
