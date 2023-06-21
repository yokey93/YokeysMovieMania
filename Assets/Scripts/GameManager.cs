using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;
    StartScreen startScreen;
    // [SerializeField] AudioClip mainThemeSong;
    AudioSource mainAudio;
    
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
        mainAudio = GetComponent<AudioSource>();
        mainAudio.Play(0);
        Debug.Log("Music shoudl play on awak");
    }

    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (quiz.isComplete)
        {
            mainAudio.Stop();
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
