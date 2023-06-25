using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Platform;

namespace GameOfLifeBasic;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    #region NotifyPropertyChangeSetup
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    public ICommand NavigateCommand { get; private set; }

    private int numRows = 600;
    private int numCols = 200;

    public int NumRows
    {
        get { return numRows; }

        set
        {
            int newValue;
            if (int.TryParse(value.ToString(), out newValue) && numRows != newValue)
            {
                numRows = newValue;
                OnPropertyChanged();
            }
        }
    }

    public int NumCols
    {
        get { return numCols; }

        set
        {
            int newValue;
            if (int.TryParse(value.ToString(), out newValue) && numCols != newValue)
            {
                numCols = newValue;
                OnPropertyChanged();
            }
        }
    }

    public MainPage()
	{
		InitializeComponent();

        NavigateCommand = new Command<Type>(
                async (Type pageType) =>
                {
                    if (KeyboardExtensions.IsSoftKeyboardShowing(rowsEntry))
                    {
                        await KeyboardExtensions.HideKeyboardAsync(rowsEntry, default);
                    }
                    if (KeyboardExtensions.IsSoftKeyboardShowing(colsEntry))
                    {
                        await KeyboardExtensions.HideKeyboardAsync(colsEntry, default);
                    }
                    //colsEntry.Unfocus();
                    Page page = (Page)Activator.CreateInstance(pageType, NumRows, NumCols);
                    await Navigation.PushAsync(page);
                });

        BindingContext = this;
    }	
}


