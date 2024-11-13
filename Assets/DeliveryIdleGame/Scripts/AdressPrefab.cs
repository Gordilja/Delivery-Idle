using UnityEngine;
using TMPro;

public class AdressPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    public void SetText(string _text) 
    {
        Text.text = _text;
    }

    public void SetSprite(Sprite _sprite) 
    {
        SpriteRenderer.sprite = _sprite;    
    }
}