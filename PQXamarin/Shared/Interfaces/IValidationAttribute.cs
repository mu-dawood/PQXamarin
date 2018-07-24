
namespace PQXamarin.Interfaces
{
    public interface IValidationAttribute
    {
        bool Validate(object Context,object Value);
        string ErrorMessage { get; set; }

        int Priority { get; set; }
    }
}
