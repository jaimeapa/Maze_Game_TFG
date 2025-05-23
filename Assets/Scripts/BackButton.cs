using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Button button;
    public GameObject mainMenu;
    public GameObject finishConditionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PressButton);
    }

    // Update is called once per frame
    public void PressButton()
    {
        mainMenu.SetActive(true);
        finishConditionsMenu.SetActive(false);
    }
}
