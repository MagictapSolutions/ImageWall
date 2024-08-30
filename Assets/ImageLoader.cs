using System.IO;
using UnityEngine;

public class ImageLoader : MonoBehaviour
{
    public string folderPath; // Path to the folder containing images
    public GameObject imagePrefab; // Prefab to display the image

    void Start()
    {
        string[] filePaths = Directory.GetFiles(folderPath, "*.jpg"); // Adjust the extension as needed

        foreach (string filePath in filePaths)
        {
            CreateImageObject(filePath);
        }
    }

    void CreateImageObject(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        // Create a new sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        // Instantiate the image prefab and assign the sprite
        GameObject newImage = Instantiate(imagePrefab, transform);
        newImage.GetComponent<SpriteRenderer>().sprite = sprite;

        // Randomize the position
        newImage.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

        // Add a movement script to the image object
        newImage.AddComponent<MoveAround>();
    }

}
