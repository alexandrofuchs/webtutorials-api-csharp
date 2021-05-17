namespace WebTutorialsApp.Persistence.Data
{
    public class WebTutorialsAppDbInitializer
    {
        public static void Initialize(WebTutorialsAppDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
