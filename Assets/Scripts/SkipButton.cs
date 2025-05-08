using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
    public Button button;
    public GameObject tutorial;
    public GameObject video;
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
        video.gameObject.SetActive(false);
        tutorial.gameObject.SetActive(false);
    }
}
