using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarManager : MonoBehaviour
{
    public Sprite[] avatarSprites;
    private int selectedAvatarIndex = 0;
    public GameObject avatar;
    public GameObject avatarBorder;

    public float pulsateScale = 1.2f; 
    public float pulsateTime = 1.0f;

    void Start()
    {
        if (PlayerPrefs.HasKey("SelectedAvatarIndex"))
        {
            selectedAvatarIndex = PlayerPrefs.GetInt("SelectedAvatarIndex");
        }

        SetAvatar(selectedAvatarIndex);
        StartPulsating();
    }

    private void StartPulsating()
    {
        // Rozpocznij pulsowanie
        LeanTween.scale(avatarBorder, Vector3.one * pulsateScale, pulsateTime)
            .setEasePunch() 
            .setLoopPingPong(); 
    }

    public void SetAvatar(int value)
    {
        if (value >= 0 && value < avatarSprites.Length)
        {
            avatar.GetComponent<Image>().sprite = avatarSprites[value];

            PlayerPrefs.SetInt("SelectedAvatarIndex", value);

            selectedAvatarIndex = value;
        }
        else
        {
            Debug.LogWarning("Invalid avatar index!");
        }
    }
}
