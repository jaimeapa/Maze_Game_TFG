using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaxTimeLimitButton : MonoBehaviour
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
        if (playerMovement.isMaxTime == true)
        {
            textUI.text = "Time Limit Activated";
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            playerMovement.isMaxTime = false;
            textUI.text = "Time Limit Deactivated";
            button.GetComponent<Image>().color = Color.white;
        }
    }
    public void PressButton()
    {
        if (playerMovement.isMaxTime == false)
        {
            playerMovement.isMaxTime = true;
            textUI.text = "Time Limit Activated";
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            playerMovement.isMaxTime = false;
            textUI.text = "Time Limit Deactivated";
            button.GetComponent<Image>().color = Color.white;
        }
    }
}
