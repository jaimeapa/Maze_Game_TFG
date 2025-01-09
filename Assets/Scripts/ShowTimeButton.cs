using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowTimeButton : MonoBehaviour
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
        
    }

    public void PressButton()
    {
        if(playerMovement.showTime == false)
        {
            playerMovement.showTime = true;
            textUI.text = "Activated";
            button.GetComponent<Image>().color = Color.blue;
        }else{
            playerMovement.showTime = false;
            textUI.text = "Deactivated";
            button.GetComponent<Image>().color = Color.white;
        }
    }
}
