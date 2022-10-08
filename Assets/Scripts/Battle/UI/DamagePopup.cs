using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Battle
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private float fadeOutTime;
        [SerializeField] private float moveSpeed;

        Transform popupTransform;

        private void Awake()
        {
            popupTransform = GetComponent<Transform>();
        }

        private void Start()
        {
            StartCoroutine(Co_FadeAway());
        }

        private IEnumerator Co_FadeAway()
        {
            float elapsedTime = 0f;

            while (elapsedTime < fadeOutTime)
            {
                popupTransform.position = Vector2.MoveTowards(popupTransform.position, (Vector2)popupTransform.position + Vector2.up, moveSpeed / 1000);
                amountText.alpha -= Time.deltaTime / fadeOutTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        public void SetText(string text)
        {
            amountText.text = text;
        }
    }
}
