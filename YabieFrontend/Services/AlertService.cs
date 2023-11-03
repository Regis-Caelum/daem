using YabieFrontend.IServices;

namespace YabieFrontend.Services;

internal class AlertService : IAlertService
{
    // ----- async calls (use with "await" - MUST BE ON DISPATCHER THREAD) -----

    public Task ShowAlertAsync(string title, string message, string cancel = "OK")
    {
        return Shell.Current.DisplayAlert(title, message, cancel);
    }

    public Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No")
    {
        return Shell.Current.DisplayAlert(title, message, accept, cancel);
    }


    // ----- "Fire and forget" calls -----

    /// <summary>
    /// "Fire and forget". Method returns BEFORE showing alert.
    /// </summary>
    public void ShowAlert(string title, string message, string cancel = "OK")
    {
        Shell.Current.Dispatcher.Dispatch(() =>
            ShowAlertAsync(title, message, cancel).GetAwaiter()
        );
    }

    /// <summary>
    /// "Fire and forget". Method returns BEFORE showing alert.
    /// </summary>
    /// <param name="callback">Action to perform afterwards.</param>
    public void ShowConfirmation(string title, string message, Action<bool> callback,
        string accept = "Yes", string cancel = "No")
    {
        Shell.Current.Dispatcher.Dispatch(() =>
        {
            var answer = ShowConfirmationAsync(title, message, accept, cancel).GetAwaiter().GetResult();
            callback(answer);
        });
    }
}