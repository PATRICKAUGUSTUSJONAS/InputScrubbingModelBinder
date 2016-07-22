namespace InputScrubbingModelBinder.Web.Example
{
    public interface IScrubberAttribute
    {
        object Scrub(string modelValue, out bool success);
    }
}
