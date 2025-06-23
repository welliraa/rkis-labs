namespace WPFLabs
{
    class UserCredentialsValidator
    {
        public enum CredentialsType { Email, Password, Name }

        private CredentialsType _credentialsType;

        public UserCredentialsValidator(CredentialsType type)
        {
            _credentialsType = type;
        }

        public bool IsValid(string value)
        {
            switch(_credentialsType)
            {
                case CredentialsType.Email:
                    return IsValidEmail(value);
                
                case CredentialsType.Password:
                    return IsValidPassword(value);

                case CredentialsType.Name:
                    return IsValidName(value);

                default:
                    return false;
            }
        }

        private bool IsValidEmail(string email)
        {
            var splitted1 = email.Split("@");

            if (splitted1.Length != 2)
            {
                return false;
            }

            var domain = splitted1[1].Split(".");

            if (domain.Length != 2)
            {
                return false;
            }

            return true;
        }

        private bool IsValidPassword(string password)
        {
            return password.Length >= 6;
        }

        private bool IsValidName(string name)
        {
            return name.Length >= 3;
        }
    }
}
