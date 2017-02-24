using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Address
{
   public class AddressSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی متقاضی مربوط به آدرس
        /// </summary>
        public Guid ApplicantId { get; set; }
    }
}
