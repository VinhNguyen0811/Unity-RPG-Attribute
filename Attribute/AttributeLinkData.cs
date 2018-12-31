using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VinhNguyen
{
    [CreateAssetMenu(fileName = "New Attribute Link Data", menuName = "Vinh Nguyen/Attribute/Link Data")]
    public class AttributeLinkData : ScriptableObject
    {
        [System.Serializable]
        public class LinkerModifier
        {
            #region Fields

            [Tooltip("Kiểu thuộc tính nhận liên kết")]
            [SerializeField]
            private AttributeType targetType;

            [Tooltip("Kiểu thuộc tính được liên kết")]
            [SerializeField]
            private AttributeType linkType;

            [Tooltip("Tỉ lệ liên kết")]
            [SerializeField]
            private float ratioValue;

            #endregion

            /// <summary>
            /// Kiểu thuộc tính nhận liên kết
            /// </summary>
            public AttributeType TargetType
            {
                get { return targetType; }
            }

            /// <summary>
            /// Kiểu thuộc tính được liên kết
            /// </summary>
            public AttributeType LinkType
            {
                get { return linkType; }
            }

            /// <summary>
            /// Hệ số liên kết
            /// </summary>
            public float RatioValue
            {
                get { return ratioValue; }
            }
        }

        [Tooltip("Các thuộc tính nhận tiền thưởng")]
        [SerializeField]
        private LinkerModifier[] linkerModifiers;

        /// <summary>
        /// Số lượng các thuộc tính nhận tiền thưởng
        /// </summary>
        public int LinkCount
        {
            get { return linkerModifiers.Length; }
        }

        /// <summary>
        /// Các thuộc tính nhận tiền thưởng
        /// </summary>
        public IEnumerable<LinkerModifier> LinkerModifiers
        {
            get { return linkerModifiers; }
        }
    }
}