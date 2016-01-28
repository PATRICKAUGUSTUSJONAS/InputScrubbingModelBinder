namespace InputScrubbingModelBinder.Web.Interfaces
{
    public interface IScrubberAttribute
    {
        object Scrub(string modelValue, out bool success);
    }
}
