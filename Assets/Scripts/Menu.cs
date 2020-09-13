using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRKeyboard.Utils;

public class Menu : MonoBehaviour
{
    
    public TextMeshProUGUI questionsId;
    public Button downloadButton;
    public Button playButton;
    public TMP_InputField questionsIdInput;
    public FirebaseController fc;
    public GameObject kb;
    public Button enter;
    public Text kbInput;
    public GameObject raycaster;

    

    // Start is called before the first frame update
    void Start()
    {
        downloadButton.onClick.AddListener(DownloadPressed);
        playButton.onClick.AddListener(PlayPressed);
        playButton.interactable = false;
        questionsIdInput.onSelect.AddListener(OpenKB);
        enter.onClick.AddListener(()=> 
        {
            gameObject.SetActive(true);
            kb.SetActive(false);
            raycaster.SetActive(false);
            questionsIdInput.text = kbInput.text;
        });
    }
    private void OpenKB(string s)
    {
        kb.SetActive(true);
        raycaster.SetActive(true);
        gameObject.SetActive(false);
    }
    private void DownloadPressed()
    {
        questionsId.text =  questionsIdInput.text + ". Please wait a second...";
        fc.GetQuestions(questionsIdInput.text);
    }
    private void PlayPressed()
    {
        SceneManager.LoadScene("Game");
    }

    
}
