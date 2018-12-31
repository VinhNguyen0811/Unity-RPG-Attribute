using UnityEngine;

namespace VinhNguyen
{
    /// <summary>
    /// Kiểu chỉnh sửa
    /// </summary>
    public enum ModifierType
    {
        /// <summary>
        /// Cộng
        /// 1 tương ứng với 1 đon vị
        /// </summary>
        Add,

        /// <summary>
        /// Nhân
        /// 1 tương ứng với 1 phần trăm
        /// </summary>
        Multiply,
    }

    /// <summary>
    /// Chỉnh sửa giá trị của AttributeValue
    /// </summary>
    [System.Serializable]
    public sealed class Modifier
    {
        #region Fields

        /// <summary>
        /// Giá trị cơ bản
        /// </summary>
        [SerializeField, Tooltip("Giá trị")]
        private float value;

        /// <summary>
        /// Kiểu chỉnh sửa
        /// </summary>
        [SerializeField, Tooltip("Kiểu chỉnh sửa")]
        private ModifierType modifierType;

        public delegate void ModifierEventHandler(Modifier modifier);

        /// <summary>
        /// Event được kích hoạt mỗi khi giá trị cuối cùng thay đổi
        /// </summary>
        public event ModifierEventHandler OnValueChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Giá trị cơ bản
        /// </summary>
        public float Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnValueChanged?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Kiểu chỉnh sửa
        /// </summary>
        public ModifierType ModifierType
        {
            get { return modifierType; }
        }
        
        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Modifier() : this(0f)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="modifierType"></param>
        public Modifier(float value, ModifierType modifierType = ModifierType.Add)
        {
            this.value = value;
            this.modifierType = modifierType;
        }

        #endregion
    }
}