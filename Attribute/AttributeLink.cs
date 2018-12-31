using UnityEngine;

namespace VinhNguyen
{
    /// <summary>
    /// Thêm tiền thưởng cho thuộc tính dựa trên giá trị của thuộc tính khác theo tỉ lệ cho trước
    /// </summary>
    [AddComponentMenu("Vinh Nguyen/Attribute Link")]
    [RequireComponent(typeof(Attribute))]
    public class AttributeLink : MonoBehaviour
    {
        [Tooltip("Các thuộc tính nhận tiền thưởng")]
        [SerializeField]
        private AttributeLinkData linkData;

        /// <summary>
        /// Các modifier tương ứng mỗi thuộc tính
        /// </summary>
        [System.NonSerialized]
        private Modifier[] modifiers;

        /// <summary>
        /// Thành phần thuộc tính
        /// </summary>
        [System.NonSerialized]
        private Attribute attribute;

        private void Awake()
        {
            attribute = GetComponent<Attribute>();

            modifiers = new Modifier[linkData.LinkCount];

            int i = 0;
            foreach (AttributeLinkData.LinkerModifier linkerModifier in linkData.LinkerModifiers)
            {
                AttributeValue targetAttributeValue = attribute.GetAttributeOfType(linkerModifier.TargetType);
                AttributeValue linkAttributeValue = attribute.GetAttributeOfType(linkerModifier.LinkType);
                linkAttributeValue.OnFinalValueChanged += LinkAttributeValue_OnFinalValueChanged;

                Modifier modifier = new Modifier(0f, ModifierType.Add);
                modifiers[i] = modifier;

                modifier.Value = linkAttributeValue.FinalValue * linkerModifier.RatioValue;

                targetAttributeValue.AddModifier(modifier);

                i++;
            }
        }

        private void OnDestroy()
        {
            if (attribute != null)
            {
                int i = 0;
                foreach (AttributeLinkData.LinkerModifier linkerModifier in linkData.LinkerModifiers)
                {
                    AttributeValue targetAttributeValue = attribute.GetAttributeOfType(linkerModifier.TargetType);
                    AttributeValue linkAttributeValue = attribute.GetAttributeOfType(linkerModifier.LinkType);
                    linkAttributeValue.OnFinalValueChanged -= LinkAttributeValue_OnFinalValueChanged;

                    targetAttributeValue.RemoveModifier(modifiers[i]);

                    i++;
                }
            }
        }

        private void LinkAttributeValue_OnFinalValueChanged(AttributeValue attributeValue)
        {
            int i = 0;
            foreach (AttributeLinkData.LinkerModifier linkerModifier in linkData.LinkerModifiers)
            {

                if (linkerModifier.LinkType == attributeValue.AttributeType)
                {
                    modifiers[i].Value = attributeValue.FinalValue * linkerModifier.RatioValue;
                }

                i++;
            }
        }
    }
}