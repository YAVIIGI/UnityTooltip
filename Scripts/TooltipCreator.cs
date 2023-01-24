using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTooltip
{
    public class TooltipCreator : MonoBehaviour
    {
        [SerializeField]
        GameObject tooltipObject;

        // Start is called before the first frame update
        void Start()
        {
            Transform tr = transform.parent;
            for (int i = 0; i < tr.childCount; i++)
            {
                Selectable ui = tr.GetChild(i).GetComponent<Selectable>();

                if (!ui)
                    continue;

                TooltipAdapter adapter = ui.GetComponent<TooltipAdapter>();

                if (!adapter)
                    continue;

                Debug.Log(ui.name);
                GameObject go = Instantiate(tooltipObject);
                go.transform.SetParent(transform);
                Tooltip tooltip = go.GetComponent<Tooltip>();
                tooltip.targetUI = ui;
                tooltip.message.text = adapter.message;
                tooltip.delay = adapter.delay;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}