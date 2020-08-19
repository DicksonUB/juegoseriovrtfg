using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Button button;
    void Start()
    {
        button.onClick.AddListener(playAgain);
    }
    public void playAgain()
    {
        SceneManager.LoadScene(1);
    }
   
}
