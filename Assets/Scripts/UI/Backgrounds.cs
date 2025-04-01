using UnityEngine;
using System.Collections.Generic;
public class Backgrounds : MonoBehaviour
{
    [SerializeField] private List<Sprite> _backgroundSprites = new List<Sprite>();
    public List<Sprite> backgroundSprites => _backgroundSprites;

    public Sprite GetBackgroundByName(string imageName)
    {
        foreach (Sprite sprite in _backgroundSprites)
        {
            if (sprite.name == imageName)
            {
                return sprite;
            }
        }
        return null;
    }



}
