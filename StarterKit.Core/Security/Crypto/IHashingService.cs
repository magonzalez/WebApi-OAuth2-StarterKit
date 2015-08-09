namespace StarterKit.Core.Security.Crypto
{
    public interface IHashingService
    {
        string CreateHashFromString(string value);
        bool ValidateStringHash(string value, string hashedValue);
    }
}
