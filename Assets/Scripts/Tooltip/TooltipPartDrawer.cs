using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tooltip
{
    public class TooltipPartDrawer : MonoBehaviour
    {
        private static string tooltipPartPrefabPath = "Tooltip/TooltipPart";
    
        private static string textPlainStringPrefab = "Tooltip/TextPlainStringPrefab";
        private static string textMainStringPrefab = "Tooltip/TextMainStringPrefab";
        private static string stringWithImagePrefab = "Tooltip/StringWithImage";
        
        private VerticalLayoutGroup verticalLayoutGroup;
        
        public static TooltipPartDrawer InitPart()
        {
            var tooltipPart = (GameObject) Instantiate(Resources.Load(tooltipPartPrefabPath));
            return tooltipPart.GetComponent<TooltipPartDrawer>();
        }
        
        public void Awake()
        {
            verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        }

        public void AddMainText(string text)
        {
            var textBox = (GameObject) Instantiate(Resources.Load(textMainStringPrefab), transform);
            var tmp = textBox.GetComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = 30;
            tmp.alignment = TextAlignmentOptions.Center;
        }
        
        public void AddPlainText(string text)
        {
            var textBox = (GameObject) Instantiate(Resources.Load(textPlainStringPrefab), transform);
            textBox.GetComponent<TextMeshProUGUI>().text = text;
        }

        // public void AddImageWithText(string text, Sprite image)
        // {
        //     var imageBox = (GameObject) Instantiate(Resources.Load(stringWithImagePrefab), transform);
        //     var drawer = imageBox.GetComponent<ImageWithTextDrawer>();
        //     drawer.ReDrawItem(image,text);
        // }
    }
}
