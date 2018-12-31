using System.Collections.Generic;
using UnityEngine;

namespace VinhNguyen
{
    
    /// <summary>
    /// Thuộc tính
    /// </summary>
    [System.Serializable]
    public sealed class AttributeValue
    {
        #region Fields

        /// <summary>
        /// Thiết lập của thuộc tính
        /// </summary>
        [Tooltip("Kiểu của thuộc tính")]
        [SerializeField]
        private AttributeType type;

        /// <summary>
        /// Giá trị cơ bản
        /// </summary>
        [SerializeField, Tooltip("Giá trị cơ bản")]
        private float baseValue;

        /// <summary>
        /// Giá trị của hiện tại thuộc tính
        /// </summary>
        [Tooltip("Giá trị của hiện tại thuộc tính")]
        [SerializeField,ReadOnly]
        private float currentValue;

        /// <summary>
        /// Giá trị cuối cùng
        /// </summary>
        [SerializeField, ReadOnly]
        private float finalValue;

        /// <summary>
        /// Danh sách chỉnh sửa
        /// </summary>
        [SerializeField, ReadOnly]
        private List<Modifier> modifiers;
        
        public delegate void AttributeEventHandler(AttributeValue attributeValue);

        /// <summary>
        /// Event được kích hoạt mỗi khi giá trị cuối cùng thay đổi
        /// </summary>
        public event AttributeEventHandler OnFinalValueChanged;

        /// <summary>
        /// Event được gọi khi current value bị thay đổi
        /// </summary>
        public event AttributeEventHandler OnCurrentValueChanged;

        /// <summary>
        /// Có gì đó đã bị thay đổi, buộc ta phải tính lại giá trị cuối cùng
        /// </summary>
        [SerializeField, ReadOnly]
        private bool changed = true;

        #endregion

        #region Properties

        /// <summary>
        /// Giá trị cơ bản
        /// </summary>
        public float BaseValue
        {
            get { return baseValue; }
        }

        /// <summary>
        /// Giá trị cuối cùng
        /// </summary>
        public float FinalValue
        {
            get
            {
                if (changed)
                {
                    CalculateFinalValue();
                }
                return finalValue;
            }
            private set
            {
                if (finalValue != value)
                {
                    finalValue = value;

                    OnFinalValueChanged?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Giá trị hiện tại của thuộc tính Attribute
        /// </summary>
        public float CurrentValue
        {
            get { return currentValue; }
            set
            {
                if (currentValue != value)
                {
                    float newValue = Mathf.Clamp(value, 0f, this.FinalValue);

                    if (newValue != currentValue)
                    {
                        currentValue = newValue;
                        OnCurrentValueChanged?.Invoke(this);
                    }
                }
            }
        }

        /// <summary>
        /// Thiết lập của thuộc tính Attribute
        /// </summary>
        public AttributeType AttributeType
        {
            get { return type; }
        }
        
        #endregion
        
        #region Methods
        
        /// <summary>
        /// Thêm một trình chỉnh sửa
        /// </summary>
        /// <param name="target"> Trình chỉnh sửa </param>
        /// <param name="update"> Có hay không tính toán lại giá trị cuối cùng ngay lập tức </param>
        public void AddModifier(Modifier target, bool update = false)
        {
            modifiers.Add(target);
            target.OnValueChanged += Modifier_OnValueChanged;

            if (update)
                CalculateFinalValue();
            else
                changed = true;
        }

        /// <summary>
        /// Xóa một trình chỉnh sửa
        /// </summary>
        /// <param name="target"> Trình chỉnh sửa </param>
        /// <param name="update"> Có hay không tính toán lại giá trị cuối cùng ngay lập tức </param>
        public void RemoveModifier(Modifier target, bool update = false)
        {
            if (modifiers.Remove(target))
            {
                target.OnValueChanged -= Modifier_OnValueChanged;
                if (update)
                    CalculateFinalValue();
                else
                    changed = true;
            }
        }
        
        /// <summary>
        /// Tính giá trị cuối cùng
        /// </summary>
        public void CalculateFinalValue()
        {
            float finalValue = baseValue;
            float totalAdd = 0f;
            float totalMultiply = 0f;

            for (int i = 0; i < modifiers.Count; i++)
            {
                Modifier mod = modifiers[i];
                if (mod == null)
                    continue;

                switch (mod.ModifierType)
                {
                    case ModifierType.Add:
                        totalAdd += mod.Value;
                        break;
                    case ModifierType.Multiply:
                        totalMultiply += mod.Value;
                        break;
                }
            }
            
            finalValue += totalAdd;
            finalValue *= (1.0f + totalMultiply);
            finalValue = Mathf.Clamp(finalValue, type.MinValue, type.MaxValue);

            FinalValue = finalValue;

            changed = false;
        }
        
        /// <summary>
        /// Callback gọi khi giá trị của modifier thay đổi, cần cập nhật lại giá trị cuối cùng
        /// </summary>
        /// <param name="modifier"></param>
        private void Modifier_OnValueChanged(Modifier modifier)
        {
            changed = true;
        }

        #endregion
    }
}