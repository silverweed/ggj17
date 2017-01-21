using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour {

    int selected = -1;
    Button[] buttons;
    JoystickListener joyLst;
    Pause pause;

    void Start() {
        buttons = new Button[2];
        buttons[0] = GameObject.Find("ResumeBtn").GetComponent<Button>();
        buttons[1] = GameObject.Find("QuitBtn").GetComponent<Button>();
        joyLst = gameObject.AddComponent<JoystickListener>();
        joyLst.OnJoystickUp += SelectNext(true);
        joyLst.OnJoystickDown += SelectNext(false);
        pause = GameObject.FindObjectOfType<Pause>();
    }

    public void Resume() {
        pause.Resume();
    }

    public void Quit() {
        SceneManager.LoadSceneAsync("Menu");
    }

    System.Action SelectNext(bool up) {
        return () => {
            if (selected < 0)
                selected = 0;
            else {
                selected = (selected + (up ? 1 : -1)) % buttons.Length;
                if (selected < 0) selected += buttons.Length;
            }

            buttons[selected].Select();
        };
    }
}
