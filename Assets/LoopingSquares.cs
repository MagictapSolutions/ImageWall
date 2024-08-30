using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoopingSquares : MonoBehaviour
{
    public string folderName = "Images"; // Folder name inside StreamingAssets
    public GameObject[] squares; // Array of square GameObjects
    public float speed = 0.5f; // Speed of movement
    public float startX = -10f; // Start position off-screen on the left
    public float endX = 10f; // End position off-screen on the right
    public float resetX = 8f; // Adjust this value to reduce the gap

    private List<Sprite> sprites = new List<Sprite>();
    private int currentImageIndex = 0;

    void Start()
    {
        StartCoroutine(LoadImagesAsync());
    }

    IEnumerator LoadImagesAsync()
    {
        string folderPath = Path.Combine(Application.streamingAssetsPath, folderName);
        string[] filePaths = Directory.GetFiles(folderPath, "*.jpg"); // Adjust the extension as needed

        if (filePaths.Length == 0)
        {
            Debug.LogError("No images found in the specified folder.");
            yield break;
        }

        foreach (string filePath in filePaths)
        {
            yield return LoadImageAsync(filePath);
        }

        // Assign the first five images to the squares
        for (int i = 0; i < squares.Length; i++)
        {
            if (i < sprites.Count)
            {
                squares[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
                squares[i].transform.localScale = Vector3.one * 0.12f;
            }
        }
    }

    IEnumerator LoadImageAsync(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        sprites.Add(sprite);

        yield return null; // Yield control back to the main thread
    }

    void Update()
    {
        if (sprites.Count == 0) return; // Prevent further execution if no images are loaded

        for (int i = 0; i < squares.Length; i++)
        {
            GameObject square = squares[i];

            // Move the square to the right
            square.transform.Translate(Vector3.right * speed * Time.deltaTime);

            // Check if the square has moved past the right edge
            if (square.transform.position.x > endX)
            {
                // Reset the position to the left side, closer to the right edge
                square.transform.position = new Vector3(resetX, square.transform.position.y, square.transform.position.z);

                // Assign the next image in the sequence
                currentImageIndex = (currentImageIndex + 1) % sprites.Count;
                square.GetComponent<SpriteRenderer>().sprite = sprites[currentImageIndex];
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(LoadImagesAsync()); // Reload images asynchronously
        }
    }
}
