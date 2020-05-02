

namespace TennisBookings.Rules
{
    public class CanNotBeEqualToZero:INumberRules
    {
        public bool Validate(int input)
        {
            return input != 0;
        }

        public string ErrorMessage => "Value cannot be equal to Zero.";
    }
}
