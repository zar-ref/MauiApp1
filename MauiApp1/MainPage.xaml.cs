using MauiApp1.SqliteRepository.Entities;
using MauiApp1.SqliteRepository.Repositories;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private readonly AccountRepository _accountRepository;
    public MainPage(AccountRepository accountRepository)
    {

        _accountRepository = accountRepository;
        InitializeComponent();
        GetAccounts();
        MessagingCenter.Subscribe<object, string>(this, "FromService", async (sender, args) =>
        {
            await Task.Delay(5000);
            //var account = new Account()
            //{
            //    CreationDate = DateTime.Now,
            //    Name = "name"
            //};
            //_accountRepository.CreateAccount(account);
            //GetAccounts();
            //await Task.Delay(10000);
        });

    }

    private void AddAccountClicked(object sender, EventArgs e)
    {
        var account = new Account()
        {
            CreationDate = DateTime.Now,
            Name = "name"
        };
        _accountRepository.CreateAccount(account);
        GetAccounts();
    }

    private void UpdateAccountClicked(object sender, EventArgs e)
    {
        if (collectionView.SelectedItem is null)
            return;

        var account = collectionView.SelectedItem as Account;
        if (account is null)
            return;

        account.CreationDate = DateTime.Now;
        _accountRepository.UpdateAccount(account);
        GetAccounts();
    }

    private void DeleteAccountClicked(object sender, EventArgs e)
    {
        if (collectionView.SelectedItem is null)
            return;

        var account = collectionView.SelectedItem as Account;
        if (account is null)
            return;

        _accountRepository.DeleteAccount(account);
        GetAccounts();
    }

    private void GetAccounts()
    {
        collectionView.ItemsSource = _accountRepository.GetAccounts();
    }
}

