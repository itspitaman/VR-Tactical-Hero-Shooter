using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingBotHeroTag : MonoBehaviour
{
    [Header("Training Bot")]
    [SerializeField] private TrainingBot bot;

    [Header("Name Text")]
    [SerializeField] private TextMeshProUGUI nameText;

    private void Start()
    {
        nameText.text = bot.heroName;
    }
}
