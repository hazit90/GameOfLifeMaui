using System;
using GameOfLifeBasic.Model;

namespace GameOfLifeBasic.Drawables;

public class BoardDrawable : IDrawable
{
    private readonly Board _board;

    public BoardDrawable(Board board)
    {
        _board = board;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float width = (float)dirtyRect.Width;
        float height = (float)dirtyRect.Height;

        Render.DrawBoard(_board, canvas, width, height);
    }

}