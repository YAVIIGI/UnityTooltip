using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UnityTooltip
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField]
        public Selectable targetUI;

        [SerializeField]
        CanvasGroup group;

        [SerializeField]
        bool isShown;

        [SerializeField]
        public Text message;

        float delayTime = 0.0f;
        float value = 0.0f;

        public float delay = 0.5f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (targetUI)
            {
                // 位置を指定の Selectable に設定
                RectTransform rt = targetUI.GetComponent<RectTransform>();
                this.GetComponent<RectTransform>().position = rt.position + new Vector3(0, rt.rect.height / 2 + 5, 0);

                // Selectable が選択状態であれば Tooltip 表示タイマーをセット
                if (EventSystem.current.currentSelectedGameObject == targetUI.gameObject)
                {
                    if (delayTime == 0.0f)
                        delayTime = Time.time;
                }
                else
                {
                    delayTime = 0.0f;
                }
            }

            Debug.Log(Time.time - delayTime);
            isShown = delayTime != 0.0f && Time.time - delayTime > delay;

            if (isShown)
            {
                value += 0.02f;
                if (value > 1.0f) value = 1.0f;
            }
            else
            {
                value -= 0.02f;
                if (value < 0.0f) value = 0.0f;
            }
            group.alpha = GetSmooth(value);
            this.GetComponent<RectTransform>().localPosition = Vector3.Lerp(this.GetComponent<RectTransform>().localPosition + Vector3.up * 10, this.GetComponent<RectTransform>().localPosition, GetSmooth(value));
        }

        float GetSmooth(float v)
        {
            return 0.5f - Mathf.Cos(v * Mathf.PI) / 2;
        }
    }

}