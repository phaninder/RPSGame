using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    private void OnDisable()
    {
        Manager.Instance.GameStart -= GetARandomGesture;
    }

    // Start is called before the first frame update
    void Start()
    {
        Manager.Instance.GameStart += GetARandomGesture;
    }

    void GetARandomGesture()
    {
        int botGesture = Random.Range((int)HandGestures.rock, (int)HandGestures.end);
        Manager.Instance.SetBotHandGesture(botGesture);
    }
}
