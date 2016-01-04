using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Address
{
   public class AddressSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی استاد مربوط به آدرس
        /// </summary>
        public Guid TeacherId { get; set; }
    }
}
