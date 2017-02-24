namespace Decision.Common.Email.Postal
{
    /// <summary>
    /// Renders an email view.
    /// </summary>
    public interface IEmailViewRenderer
    {
        string Render(Email email, string viewName = null);
    }
}
