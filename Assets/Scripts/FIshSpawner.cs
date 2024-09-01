using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIshSpawner : MonoBehaviour
{
    public Sprite[] creatureSprites;
    public float maxCount, SpawnTimer, currentTime;
    public bool canSpawn;
    public GameObject fishPrefab;

    public SpriteRenderer area;
    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            if (SpawnTimer > currentTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                if (transform.childCount < maxCount)
                {
                    currentTime = 0;
                    SpawnRandomFish();
                }
            }
        }
    }
    Vector3 GetRandomPointInBounds()
    {
        Bounds bounds = area.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(randomX, randomY,0);
    }
    private void SpawnRandomFish()
    {
        int randIndex = Random.Range(0, creatureSprites.Length);
        GameObject Fish = Instantiate(fishPrefab);
        Fish.transform.parent = this.transform;
        Fish.transform.position = GetRandomPointInBounds();
        Fish.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        Fish.GetComponent<Fishy>()._spriteRenderer.sprite = creatureSprites[randIndex];
    }
}
