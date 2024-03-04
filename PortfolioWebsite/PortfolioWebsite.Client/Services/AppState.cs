namespace PortfolioWebsite.Client.Services
{
    public enum CurrentPage
    {
        AboutMe = 1,
        Projects = 2,
        Contact = 3
    }
    public class AppState
	{
        public CurrentPage currentPage = CurrentPage.AboutMe;

        public void SetCurrentPage(CurrentPage page)
        {
            currentPage = page;
            OnChange?.Invoke();
        }

        public event Action OnChange;
    }

}
