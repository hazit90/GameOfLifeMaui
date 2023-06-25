

using System.ComponentModel;
using GameOfLifeBasic.Drawables;
using GameOfLifeBasic.ViewModel;



namespace GameOfLifeBasic.Views;
public partial class GameCanvas : ContentPage
{
    private GameCanvasVM gameCanvasVM;

    public GameCanvas(int numRows, int numCols)
    {
        InitializeComponent();

        // Initialize the ViewModel
        gameCanvasVM = new GameCanvasVM(numRows, numCols);
        BindingContext = gameCanvasVM;

        // Subscribe to the PropertyChanged event
        gameCanvasVM.PropertyChanged += ViewModel_PropertyChanged;

        // Start the timer in the ViewModel
        gameCanvasVM.StartTimer();
    }

    private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // If the Board property has changed, update the Drawable for the canvasView
        if (e.PropertyName == nameof(gameCanvasVM.Board))
        {
            // Update Drawable with the new board
            canvasView.Drawable = new BoardDrawable(gameCanvasVM.Board);
        }
    }

    void ContentPage_NavigatedFrom(System.Object sender, Microsoft.Maui.Controls.NavigatedFromEventArgs e)
    {
        gameCanvasVM.StopTimer();
    }
}

