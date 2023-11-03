using YabieFrontend.ViewModels;

namespace YabieFrontend.UI.Pages;

public partial class VerifyOtpPage
{
    public VerifyOtpPage(VerifyOtpViewModel verifyOtpViewModel)
    {
        InitializeComponent();
        BindingContext = verifyOtpViewModel;
    }

    private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;

        if (string.IsNullOrEmpty(e.NewTextValue) || e.NewTextValue.Length != 1) return;
        
        var nextEntry = GetNextEntry(entry);
        nextEntry?.Focus();
    }
    
    private Entry GetNextEntry(ITextAlignment entry)
    {
        if (entry == Entry1)
        {
            return Entry2;
        }

        if (entry == Entry2)
        {
            return Entry3;
        }

        if (entry == Entry3)
        {
            return Entry4;
        }

        if (entry == Entry4)
        {
            return Entry5;
        }

        return entry == Entry5 ? Entry6 : null; 
    }
}