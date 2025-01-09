using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button button;
    public GameObject menu;
    public GameObject mazesSavedMenu;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PressButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PressButton()
    {
        mazesSavedMenu.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }
}
