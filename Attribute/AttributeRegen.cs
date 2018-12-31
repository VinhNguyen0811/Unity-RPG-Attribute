using UnityEngine;

namespace VinhNguyen
{    /// <summary>
     /// Thêm tiền thưởng cho giá trị CurrentValue thuộc tính dựa trên giá trị của thuộc tính khác theo tỉ lệ cho trước
     /// </summary>
    [AddComponentMenu("Vinh Nguyen/Attribute Regen")]
    [RequireComponent(typeof(Attribute))]
    public class AttributeRegen : MonoBehaviour
    {
        /// <summary>
        /// Thời gian mỗi lần mà việc phục hồi được lặp lại
        /// </summary>
        private const float TIME_REGEN_REPEAT = 5f;

        [Tooltip("Các thuộc tính nhận tiền thưởng")]
        [SerializeField]
        private AttributeRegenData regenData;

        /// <summary>
        /// Mảng thuộc tính nhận được phục hồi
        /// </summary>
        [System.NonSerialized]
        private AttributeValue[] targetAttributies;

        /// <summary>
        /// Mảng thuộc tính để lấy giá trị phục hồi
        /// </summary>
        [System.NonSerialized]
        private AttributeValue[] regenAttributies;

        /// <summary>
        /// Lần phục hồi tiếp theo
        /// </summary>
        [System.NonSerialized]
        private float nextTimeRegen;

        private void Awake()
        {
            Attribute attributeComponent = GetComponent<Attribute>();

            targetAttributies = new AttributeValue[regenData.RegenerationCount];
            regenAttributies = new AttributeValue[regenData.RegenerationCount];

            int i = 0;
            foreach (AttributeRegenData.Regeneration regen in regenData.Regenerations)
            {
                targetAttributies[i] = attributeComponent.GetAttributeOfType(regen.TargetType);
                regenAttributies[i] = attributeComponent.GetAttributeOfType(regen.RegenType);

                i++;
            }
        }

        private void Update()
        {
            if (Time.time >= nextTimeRegen)
            {
                nextTimeRegen = Time.time + TIME_REGEN_REPEAT;

                for (int i = 0; i < targetAttributies.Length; i++)
                {
                    targetAttributies[i].CurrentValue += regenAttributies[i].FinalValue;
                }
            }
        }
    }
}