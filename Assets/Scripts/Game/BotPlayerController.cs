using System;
using System.Collections;
using UnityEngine;

public class BotPlayerController : MonoBehaviour {
    private void OnEnable() {
        StartCoroutine(RandomPlayCR());
    }

    private IEnumerator RandomPlayCR()
    {
        throw new NotImplementedException();
    }
}