using System.Collections.Generic;
using UnityEngine;

namespace VinhNguyen
{
    [AddComponentMenu("Vinh Nguyen/Attribute")]
    public class Attribute : MonoBehaviour
    {
        /// <summary>
        /// Mảng thuộc tính
        /// </summary>
        [SerializeField, Tooltip("Mảng thuộc tính")]
        private AttributeValue[] attributies = new AttributeValue[20];

        [System.NonSerialized]
        private Dictionary<AttributeType, AttributeValue> attributiesDict;

        /// <summary>
        /// Mảng thuộc tính
        /// </summary>
        public AttributeValue[] Attributes
        {
            get { return attributies; }
        }

        /// <summary>
        /// Lấy attribute có kiểu là type
        /// </summary>
        public AttributeValue GetAttributeOfType(AttributeType type)
        {
            AttributeValue result;

            if (attributiesDict == null)
            {
                attributiesDict = new Dictionary<AttributeType, AttributeValue>(attributies.Length);

                for (int i = 0; i < attributies.Length; i++)
                {
                    AttributeValue attributeValue = attributies[i];
                    attributiesDict.Add(attributeValue.AttributeType, attributeValue);
                }

#if !UNITY_EDITOR
                attributies = null;
#endif
            }

            attributiesDict.TryGetValue(type, out result);

            return result;
        }
    }
}