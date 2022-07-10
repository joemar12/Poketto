namespace Poketto.Application.Common
{
    public abstract class BasePayload<PayloadType, ErrorType> : IBasePayload 
        where PayloadType : BasePayload<PayloadType, ErrorType>, new()
        where ErrorType : IBaseError
    {
        public BasePayload()
        {
            Errors = new List<ErrorType>();
        }

        public List<ErrorType> Errors { get; set; }

        [GraphQLIgnore]
        public PayloadType PushError(params ErrorType[] errors)
        {
            this.Errors.AddRange(errors);

            return (PayloadType)this;
        }

        [GraphQLIgnore]
        public static PayloadType Error(params ErrorType[] errors)
        {
            PayloadType u = new();
            u.Errors.AddRange(errors);
            return u;
        }

        [GraphQLIgnore]
        public static PayloadType Success()
        {
            return new PayloadType();
        }

        [GraphQLIgnore]
        public void AddError(IBaseError error)
        {

            if (error is ErrorType)
            {
                ErrorType tmp = (ErrorType)error;
                Errors.Add(tmp);
            }
            else
            {
                throw new NotSupportedException("Error type does not match base payload supported types");
            }
        }
    }
}
