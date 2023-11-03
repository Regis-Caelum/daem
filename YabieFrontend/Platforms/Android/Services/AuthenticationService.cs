using System.Diagnostics;
using Firebase;
using Firebase.Auth;
using Java.Lang;
using Java.Util.Concurrent;
using YabieFrontend.IServices;

namespace YabieFrontend.Services;

public class AuthenticationService : PhoneAuthProvider.OnVerificationStateChangedCallbacks, IAuthenticationService
{
    private TaskCompletionSource<bool> _verificationCompletionSource;
    private string _verificationId;

    public Task<bool> AuthenticatePhoneNumber(string phoneNumber)
    {
        _verificationCompletionSource = new TaskCompletionSource<bool>();

        
            var authOption = PhoneAuthOptions
                .NewBuilder()
                .SetPhoneNumber(phoneNumber)
                .SetTimeout((Long)60, TimeUnit.Seconds)
                .SetActivity(Platform.CurrentActivity)
                .SetCallbacks(this)
                .Build();

            PhoneAuthProvider.VerifyPhoneNumber(authOption);
        

        return _verificationCompletionSource.Task;
    }

    public Task<bool> SendOtp(string otp)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> VerifyOtp(string phoneNumber, string otp)
    {
        bool returnValue = false;
        if (!string.IsNullOrWhiteSpace(otp))
        {
            var credentials = PhoneAuthProvider.GetCredential(_verificationId, otp);
            await FirebaseAuth.Instance.SignInWithCredentialAsync(credentials).ContinueWith(authTask =>
            {
                if (authTask.IsFaulted || authTask.IsCanceled)
                {
                    returnValue = false;
                    return;
                }

                returnValue = true;
            });
        }

        return returnValue;
    }

    public override void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken p1)
    {
        base.OnCodeSent(verificationId, p1);
        _verificationCompletionSource.SetResult(true);
        _verificationId = verificationId;
    }

    public override void OnVerificationCompleted(PhoneAuthCredential p0)
    {
        Debug.WriteLine("Verification Completed");
    }

    public override void OnVerificationFailed(FirebaseException p0)
    {
        _verificationCompletionSource.SetResult(false);
    }
}