namespace Zinnia.Data.Type.Transformation.Conversion
{
    using UnityEngine.Events;
    using System;

    public class NotBoolean : Transformer<bool, bool, NotBoolean.UnityEvent>
    {
        /// <summary>
        /// Defines the event with the transformed <see cref="bool"/> value.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<bool>
        {
        }

        protected override bool Process(bool input)
        {
            return !input;
        }
    }
}
