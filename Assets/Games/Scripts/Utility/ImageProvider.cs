using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageProvider : MonoBehaviour
{
    [SerializeField] private List<ImageData> imagesData = new List<ImageData>();

    protected readonly Dictionary<string, Sprite> dictionary = new Dictionary<string, Sprite>();

    private void Awake()
    {
        foreach (ImageData iData in imagesData)
        {
            dictionary.Add(iData.spriteID, iData.sprite);
        }
    }

    public Sprite RequestSprite(string spriteID)
    {
        if (dictionary.ContainsKey(spriteID)) return dictionary[spriteID];
        else return null;
    }

    [System.Serializable]
    public class ImageData
    {
        public Sprite sprite;
        public string spriteID;
    }
}
