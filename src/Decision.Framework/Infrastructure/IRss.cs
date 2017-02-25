using System;

namespace Decision.Framework.Infrastructure
{
    public interface IRss
    {
        string Title { get; set; }
        string Description { get; set; }
        DateTime CreatedAt { get; set; }
        Uri Link { get; set; }
        string Author { get; set; }
    }
}