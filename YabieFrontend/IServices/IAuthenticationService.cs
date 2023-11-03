namespace YabieFrontend.IServices;

public interface IAuthenticationService
{
    Task<bool> AuthenticatePhoneNumber(string phoneNumber);
    Task<bool> VerifyOtp(string phoneNumber, string otp);
    
    Task<bool> SendOtp(string otp);
}