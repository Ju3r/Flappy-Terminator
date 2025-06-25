using System;

public class RestartWindow : Window
{
    public event Action RestartButtonClicked;

    protected override void OnButtonClick()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        RestartButtonClicked?.Invoke();
    }
}
