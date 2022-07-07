using System;
using System.Collections;
using UnityEngine;

public class BotPlayerController : MonoBehaviour {
    [SerializeField] private float randomTimeMin, randomTimeMax;
    [SerializeField] private Collider botRegion;
    [SerializeField] private Transform fieldRoot;

    private GameController gameController;
    private void Awake() {
        gameController = GetComponent<GameController>();
    }

    private void OnEnable() {
        StartCoroutine(RandomPlayCR());
    }

    private IEnumerator RandomPlayCR()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(randomTimeMin, randomTimeMax));
            gameController.SpawnTopSoldier(GetRandomPositionInField());
        }
    }

    private Vector3 GetRandomPositionInField()
    {
        var bounds = botRegion.bounds;
        var inBoundRandom = new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
        var onCollider = botRegion.ClosestPoint(inBoundRandom);
        RaycastHit hit;
        botRegion.Raycast(new Ray(onCollider + botRegion.transform.up*10, -botRegion.transform.up), out hit, Mathf.Infinity);
        var fieldLocation = fieldRoot.InverseTransformPoint(hit.point);
        return fieldLocation;
    }
}