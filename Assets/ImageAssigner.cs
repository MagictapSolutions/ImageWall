using System.Collections;
using System.IO;
using UnityEngine;

public class ImageAssigner : MonoBehaviour
{
    public string folderPath; // Path to the folder containing images
    public GameObject[] squares; // Array of 11 square GameObjects
    public float moveDuration = 5f; // Duration to move each image

    private Texture2D[] textures;
    private Sprite[] sprites;

    void Start()
    {
        // Load the first 11 images from the folder
        LoadImages();

        // Start the sequence of assigning and moving images
        StartCoroutine(AssignAndMoveImages());
    }

    void LoadImages()
    {
        string[] filePaths = Directory.GetFiles(folderPath, "*.jpg"); // Adjust the extension as needed

        textures = new Texture2D[11];
        sprites = new Sprite[11];

        for (int i = 0; i < 10; i++)
        {
            byte[] fileData = File.ReadAllBytes(filePaths[i]);
            textures[i] = new Texture2D(2, 2);
            textures[i].LoadImage(fileData);
            sprites[i] = Sprite.Create(textures[i], new Rect(0, 0, textures[i].width, textures[i].height), new Vector2(0.5f, 0.5f));
        }
    }

    IEnumerator AssignAndMoveImages()
    {
        for (int i = 0; i < 11; i++)
        {
            // Create a temporary GameObject to display the sprite and move it
            GameObject tempImage = new GameObject("TempImage");
            SpriteRenderer sr = tempImage.AddComponent<SpriteRenderer>();
            sr.sprite = sprites[i];
            sr.transform.localScale = new Vector3(0.2f, 0.2f, 1f);


            // Randomize the starting position
            tempImage.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

            // Move the image around for the specified duration
            float timer = 0f;
            while (timer < moveDuration)
            {
                tempImage.transform.Translate(new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f) * Time.deltaTime*30);
                timer += Time.deltaTime;
                yield return null;
            }

            // Assign the sprite to the corresponding square GameObject
            squares[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
            squares[i].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);

            // Destroy the temporary GameObject
            Destroy(tempImage);
        }
    }
}
