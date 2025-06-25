using System;

public class StartWindow : Window
{
    public event Action PlayButtonClicked;

    protected override void OnButtonClick()
    {
        StartGame();
    }

    private void StartGame()
    {
        PlayButtonClicked?.Invoke();
    }
}
