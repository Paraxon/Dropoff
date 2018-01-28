using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class GUIManager : MonoBehaviour {

    // GUI Screens
    public GameObject scrMainMenu;
    public List<GameObject> scrMainMenuObjects;
    public GameObject scrCredits;
    public GameObject scrGameplay;
    public List<GameObject> scrGameplayObjects;
    public GameObject scrPauseMenu;
    public List<GameObject> scrPauseObjects;
    public GameObject scrGameOver;
    public List<GameObject> scrGameOverObjects;

    // Camera change
    public Canvas GUICanvas;
    public Camera gameCamera;

    private AudioSource audio;

    void Start() {

        // Set up GUI screens
        scrMainMenu.active = true;
        scrCredits.active = false;
        scrGameplay.active = false;
        scrPauseMenu.active = false;
        scrGameOver.active = false;

        audio = GetComponentInChildren<AudioSource>();
        audio.Play();

    }

    void Update() {

        // Keyboard shortcuts to access GUI screens
        if (scrMainMenu.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                OnGameplay();
            }
        }
        if (scrGameplay.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                OnPause();
            }
        }
        if (scrPauseMenu.activeSelf && scrGameplay.active == false) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                OnUnpause();
            }
        }
        if (scrGameOver.activeSelf && scrGameplay.active == false) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                OnRestart();
            }
        }

    }

    // View credits
    public void OnCredits() {

        foreach (GameObject objects in scrMainMenuObjects) {
            objects.SetActive(false);
        }
        StartCoroutine(FadeSwitch(scrMainMenu, scrCredits));

    }

    // Back to Main Menu
    public void OnMainMenu() {

        StartCoroutine(FadeSwitch(scrCredits, scrMainMenu));
        foreach (GameObject objects in scrMainMenuObjects) {
            objects.SetActive(true);
        }

    }

    // Start game
    public void OnGameplay() {

        audio.Stop();
        GameObject.Find("Player").GetComponent<ThirdPersonUserControl>().enabled = true;
        GameObject.Find("Camera").SetActive(false);
        GameObject.Find("FreeLookCameraRig").SetActive(true);
        GUICanvas.worldCamera = gameCamera;
        foreach (GameObject objects in scrMainMenuObjects) {
            objects.SetActive(false);
        }
        StartCoroutine(FadeSwitch(scrMainMenu, scrGameplay));

    }    

    // Pause game
    public void OnPause() {

        foreach (GameObject objects in scrGameplayObjects) {
            objects.SetActive(false);
        }
        StartCoroutine(FadeSwitch(scrGameplay, scrPauseMenu));
        foreach (GameObject objects in scrPauseObjects) {
            objects.SetActive(true);
        }

    }

    // Unpause game
    public void OnUnpause() {

        foreach (GameObject objects in scrPauseObjects) {
            objects.SetActive(false);
        }
        StartCoroutine(FadeSwitch(scrPauseMenu, scrGameplay));
        foreach (GameObject objects in scrGameplayObjects) {
            objects.SetActive(true);
        }

    }

    // Game over
    public void OnGameOver() {
        //Time.timeScale = 0;
        foreach (GameObject objects in scrGameplayObjects) {
            objects.SetActive(false);
        }
        StartCoroutine(FadeSwitch(scrGameplay, scrGameOver));
        foreach (GameObject objects in scrGameOverObjects) {
            objects.SetActive(true);
        }

    }

    // Restart game from Game Over screen
    public void OnRestart() {

        SceneManager.LoadScene(0);

    }

    // Fade switch; takes time to fade between screens
    IEnumerator FadeSwitch(GameObject firstScr, GameObject secondScr) {

        firstScr.FadeOut();
        yield return new WaitForSeconds(0.25f);
        secondScr.FadeIn();

    }

}
