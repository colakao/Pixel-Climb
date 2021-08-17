using System.Collections.Generic;
using UnityEngine;
using Player;
using Core.Audio;
using Core;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner Instance { get; private set; }

    public GameObject platformPrefab;
    List<GameObject> platforms = new List<GameObject>();

    public float nextPlatformHeight = 6.75f;
    public int platformsBeforeDeleting = 8;
    public int maxPlatformsInList = 4;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        PlatformSpawnPosition(0);
    }

    void PlatformSpawnPosition(float currentPlatformHeight)
    {
        //Get position to spawn
        float minX = WorldBorder.Instance.minX;
        float maxX = WorldBorder.Instance.maxX;
        float randomX = Random.Range(minX, maxX);

        Vector2 platformPosition = new Vector2(randomX, currentPlatformHeight + nextPlatformHeight);

        GameObject platform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);

        platform.transform.position = new Vector2(
            Mathf.Clamp(platform.transform.position.x, minX + platform.GetComponent<SpriteRenderer>().size.x / 2, maxX - platform.GetComponent<SpriteRenderer>().size.x / 2),
            platform.transform.position.y
            );
        platform.transform.parent = transform;
        platforms.Add(platform.gameObject);

        //Increase score
        if (platforms.Count <= 1) return;

        //Swap platform for yellow one when passed
        platforms[platforms.Count - 2].GetComponent<PlatformSpriteSwap>().Swap();

        GameManager.Instance.IncreaseScore(1);
        AudioManager.Instance.PlayAudio(AudioTypeName.SFX_01);
    }

    public void RemoveAllPlatforms()
    {
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            GameObject go = platforms[i];
            platforms.Remove(go);
            Destroy(go);
        }
        PlatformSpawnPosition(0);
    }

    // the name is deceitful. This method actually returns the second to last platform
    public Vector3 GetTopPlatform()
    {
        if (platforms.Count < 2) return Vector3.zero;

        Collider2D platformCol = platforms[platforms.Count - 2].GetComponent<Collider2D>();
        Collider2D playerCol = PlayerController.Instance.GetComponent<Collider2D>();
        Vector2 platformSurface = (Vector2)platformCol.bounds.center + new Vector2(0, platformCol.bounds.extents.y);
        Vector2 playerHalfHeight = (Vector2)playerCol.bounds.extents;
        playerHalfHeight.x = 0;

        return platformSurface + playerHalfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (platforms.Count < 0) return;

        if (PlayerController.Instance.transform.position.y >= platforms[platforms.Count - 1].transform.position.y && platforms.Count > 0)
        {
            PlatformSpawnPosition(platforms[platforms.Count - 1].transform.position.y);
        }
        if (platforms[platforms.Count - 1].transform.position.y >= nextPlatformHeight * platformsBeforeDeleting)
        {
            if (platforms.Count >= maxPlatformsInList)
            {
                for (int i = 0; i < platforms.Count - (maxPlatformsInList); i++)
                {
                    GameObject last = platforms[i];
                    platforms.Remove(last);
                    Destroy(last);
                }
            }
        }
    }
}