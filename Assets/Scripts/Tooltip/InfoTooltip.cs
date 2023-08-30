using UnityEngine;

namespace Tooltip
{
    public class InfoTooltip : TooltipInstance, ITooltipPart
    {
        [SerializeField] private string name;
        [SerializeField] private string description;
        public TooltipPartDrawer GetTooltipPart()
        {
            var part = TooltipPartDrawer.InitPart();
            part.AddMainText(name);
            part.AddPlainText(description);
            return part;
        }
    }
}