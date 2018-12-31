using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VinhNguyen
{
    [CreateAssetMenu(fileName = "New Attribute Point Data", menuName = "Vinh Nguyen/Attribute/Point Data")]
    public class AttributePointData : ScriptableObject
    {
        [System.Serializable]
        public class PointModifier
        {
            [Tooltip("Kiểu thuộc tính nhận thưởng theo điểm thuộc tính")]
            [SerializeField]
            private AttributeType type;

            [Tooltip("Tiền thưởng cho mỗi điểm")]
            [SerializeField]
            private float bonus;

            /// <summary>
            /// Kiểu thuộc tính nhận thưởng theo điểm thuộc tính
            /// </summary>
            public AttributeType Type
            {
                get { return type; }
            }

            /// <summary>
            /// Tiền thưởng cho điểm
            /// </summary>
            public float Bonus
            {
                get { return bonus; }
            }
        }

        [Tooltip("Các thuộc tính nhận tiền thưởng")]
        [SerializeField]
        private PointModifier[] pointModifiers;

        public int AttributePointCount
        {
            get { return pointModifiers.Length; }
        }

        public IEnumerable<PointModifier> PointModifiers
        {
            get { return pointModifiers; }
        }
    }
}