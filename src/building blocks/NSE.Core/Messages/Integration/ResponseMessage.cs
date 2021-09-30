using FluentValidation.Results;

namespace NSE.Core.Messages.Integration
{
    public class ResponseMessage : Message
    {
        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult {get; set;}
    }
}
