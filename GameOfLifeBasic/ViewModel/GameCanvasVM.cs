using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GameOfLifeBasic.Drawables;
using GameOfLifeBasic.Model;
using Microsoft.Maui;

namespace GameOfLifeBasic.ViewModel;

public class GameCanvasVM : INotifyPropertyChanged
{
    #region PropertyChangedSetup
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    private Board board;
    private BoardDrawable boardDrawable;
    private Timer timer;

    public GameCanvasVM(int numCols, int numRows)
    {
        board = new Board(numCols, numRows);

        // Using System.Threading.Timer
        timer = new Timer(OnTimerElapsed, null, 0, 100);
    }

    public Board Board
    {
        get { return board; }
        set
        {
            if (board != value)
            {
                board = value;
                //OnPropertyChanged(nameof(Board));
            }
        }
    }

    public BoardDrawable B_BoardDrawable
    {
        get { return boardDrawable; }
        set
        {
            boardDrawable = value;
            OnPropertyChanged();
        }
    }


    public void StartTimer()
    {
        // Start the timer with an interval of 100ms
        timer = new Timer(OnTimerElapsed, null, 0, 100);
    }
    public void StopTimer()
    {
        // Stop the timer
        timer?.Dispose();
        timer = null;
    }

    private async void OnTimerElapsed(object state)
    {
        try
        {
            await Task.Run(() => {
                board.Advance();
            });

            // Notify the view that the Board has been updated
            //OnPropertyChanged(nameof(Board));
            B_BoardDrawable = new BoardDrawable(board);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
