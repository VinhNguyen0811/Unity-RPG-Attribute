using UnityEngine;

namespace VinhNguyen
{
    /// <summary>
    /// Thiết lập cho một thuộc tính
    /// </summary>
    [CreateAssetMenu(fileName = "New Attribute Type", menuName = "Vinh Nguyen/Attribute/Type")]
    public sealed class AttributeType : Profile
    {
        #region Fields

        [Tooltip("Giá trị nhỏ nhất của thuộc tính")]
        [SerializeField]
        private float minValue;

        [Tooltip("Giá trị lớn nhất của thuộc tính")]
        [SerializeField]
        private float maxValue;

        #endregion

        #region Properties

        /// <summary>
        /// Giá trị nhỏ nhất
        /// </summary>
        public float MinValue
        {
            get { return minValue; }
        }

        /// <summary>
        /// Giá trị lớn nhất
        /// </summary>
        public float MaxValue
        {
            get { return maxValue; }
        }

        #endregion
    }
}