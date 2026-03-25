using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelOrb : MonoBehaviour
{
        public Slider slider;
        public TextMeshProUGUI levelText;

        private void Start()
        {
                slider = GetComponent<Slider>();
                levelText = GetComponentInChildren<TextMeshProUGUI>();
        }
}
