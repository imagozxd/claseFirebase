using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts
{
    public class HandlerUI : MonoBehaviour
    {
        [Header("Custom Components")]
        [SerializeField] private TMP_InputField fieldAttribute;
        [SerializeField] private TMP_Text attributeText;

        [Header("Text Components")]
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text lastnameText;
        [SerializeField] private TMP_Text idText;

        [Header("Database Components")]
        [SerializeField] private DatabaseHandler databaseHandler;
        public void GetAttributeInformation()
        {
            string custom = fieldAttribute.textComponent.text;
            custom = custom.Remove(custom.Length - 1);
            Debug.Log(custom);
            StartCoroutine(databaseHandler.GetCustomAttribute(custom, UpdateCustomText));
        }
        private void UpdateCustomText(string newText)
        {
            attributeText.text = newText;
        }

        public void GetName()
        {
            StartCoroutine(databaseHandler.GetFirstName(ChangeName));
        }
        private void ChangeName(string newName)
        {
            nameText.text = newName;    
        }


        public void GetLastname()
        {
            StartCoroutine(databaseHandler.GetLastName(ChangeLastName));
        }
        private void ChangeLastName(string newLastName)
        {
            nameText.text = newLastName;
        }


        public void GetId()
        {
            StartCoroutine(databaseHandler.GetCodeID(ChangeId));
        }
        private void ChangeId(int newIdw)
        {
            nameText.text = newIdw.ToString();
        }
    }
}