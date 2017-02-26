using System.Collections.Generic;

namespace Decision.ViewModels.EducationalBackground
{
    public class EducationalBackgroundListViewModel
    {
        public EducationalBackgroundSearchRequest SearchRequest { get; set; }

        public IEnumerable<EducationalBackgroundViewModel> EducationalBackgrounds { get; set; }

    }
}