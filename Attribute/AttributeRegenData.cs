using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VinhNguyen
{
    [CreateAssetMenu(fileName = "New Attribute Regen Data", menuName = "Vinh Nguyen/Attribute/Regen Data")]
    public class AttributeRegenData : ScriptableObject
    {
        [System.Serializable]
        public class Regeneration
        {
            #region Fields

            [Tooltip("Kiểu của thuộc tính nhận được phục hồi")]
            [SerializeField]
            private AttributeType targetType;

            [Tooltip("Kiểu thuộc tính để lấy giá trị phục hồi")]
            [SerializeField]
            private AttributeType regenType;

            #endregion

            #region Properties

            /// <summary>
            /// Kiểu của thuộc tính nhận được phục hồi
            /// </summary>
            public AttributeType TargetType
            {
                get { return targetType; }
            }

            /// <summary>
            /// Kiểu thuộc tính để lấy giá trị phục hồi
            /// </summary>
            public AttributeType RegenType
            {
                get { return regenType; }
            }

            #endregion
        }
        
        [Tooltip("Các thuộc tính nhận tiền thưởng")]
        [SerializeField]
        private Regeneration[] regeneraties;

        /// <summary>
        /// Số lượng các thuộc tính nhận tiền thưởng
        /// </summary>
        public int RegenerationCount
        {
            get { return regeneraties.Length; }
        }

        /// <summary>
        /// Các thuộc tính nhận tiền thưởng
        /// </summary>
        public IEnumerable<Regeneration> Regenerations
        {
            get { return regeneraties;}
        }
    }
}