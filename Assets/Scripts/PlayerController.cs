using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Button[] playerInputButtons;

    private void OnDisable()
    {
        Manager.Instance.GameStart -= ShowPlayerInput;
        Manager.Instance.GameEnd -= HidePlayerInput;
    }

    // Start is called before the first frame update
    void Start()
    {
        Manager.Instance.GameStart += ShowPlayerInput;
        Manager.Instance.GameEnd += HidePlayerInput;
    }

    public void ShowPlayerInput()
    {
        foreach (Button obj in playerInputButtons)
        {
            obj.interactable = true;
        }
    }

    public void HidePlayerInput()
    {
        foreach (Button obj in playerInputButtons)
        {
            obj.interactable = false;
        }
    }
    public void SetPlayerInput(int gesture)
    {
        HidePlayerInput();
        Manager.Instance.SetPlayerHandGesture(gesture);
    }
}
