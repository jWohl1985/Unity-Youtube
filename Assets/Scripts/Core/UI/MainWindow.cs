using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MainWindow : MonoBehaviour
    {
        void Start()
        {
            ShowDefaultView();
        }

        private void ShowDefaultView()
        {
            foreach(Transform child in transform)
            {
                if (child.GetComponent<PartyMemberInfo>() != null)
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }
        }
    }
}
