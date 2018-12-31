using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VinhNguyen
{
    [CreateAssetMenu(fileName = "New Attribute Level Data", menuName ="Vinh Nguyen/Attribute/Level Data")]
    public class AttributeLevelData : ScriptableObject
    {
        [System.Serializable]
        public class LevelModifier
        {
            [Tooltip("Kiểu thuộc tính nhận thưởng theo cấp độ nhân vật")]
            [SerializeField]
            private AttributeType type;

            [Tooltip("Tiền thưởng cho mỗi cấp độ")]
            [SerializeField]
            private float bonus;

            /// <summary>
            /// Kiểu thuộc tính nhận thưởng theo cấp độ nhân vật
            /// </summary>
            public AttributeType Type
            {
                get { return type; }
            }

            /// <summary>
            /// Tiền thưởng cho mỗi cấp độ
            /// </summary>
            public float Bonus
            {
                get { return bonus; }
            }
        }

        [Tooltip("Các thuộc tính nhận tiền thưởng")]
        [SerializeField]
        private LevelModifier[] levelModifiers;

        /// <summary>
        /// Số lượng các thuộc tính nhận tiền thưởng
        /// </summary>
        public int AttributeLevelCount
        {
            get { return levelModifiers.Length; }
        }

        /// <summary>
        /// Các thuộc tính nhận tiền thưởng
        /// </summary>
        public IEnumerable<LevelModifier> LevelModifiers
        {
            get { return levelModifiers; }
        }
    }
}