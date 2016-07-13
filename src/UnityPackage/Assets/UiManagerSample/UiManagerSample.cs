using System.Collections;
using Common.Logging;
using UnityEngine;

public class UiManagerSample : MonoBehaviour
{
    protected ILog _logger = LogManager.GetLogger("UiManagerSample");

    protected void Awake()
    {
        UiManager.Initialize();
    }

    public void OnMessageBoxClick()
    {
        UiMessageBox.Show("This is a message <b>box</b>", _ =>
        {
            _logger.Info("MessageBox done");
        });
    }

    public void OnMessageQuestionBoxClick()
    {
        UiMessageBox.Show("This is a message question <b>box</b>", UiMessageBox.QuestionType.OkCancel, r =>
        {
            _logger.InfoFormat("MessageQuestionBox done with {0}", r);
        });
    }

    public void OnMessageQuestionBoxCoroutineClick()
    {
        StartCoroutine(ShowMessageQuestionBoxCoroutine());
    }

    private IEnumerator ShowMessageQuestionBoxCoroutine()
    {
        while (true)
        {
            var handle = UiMessageBox.Show("Do you want to continue?", UiMessageBox.QuestionType.ContinueStop);
            yield return StartCoroutine(handle.WaitForHide());
            _logger.Info(handle.ReturnValue);
            if ((UiMessageBox.QuestionResult)handle.ReturnValue == UiMessageBox.QuestionResult.Continue)
            {
                var handle2 = UiMessageBox.Show("Let's do it again");
                yield return StartCoroutine(handle2.WaitForHide());
            }
            else
            {
                UiMessageBox.Show("Done");
                yield break;
            }
        }
    }

    public void OnInputBoxClick()
    {
        UiInputBox.Show("Please input your name:", "Bill", callback: value =>
        {
            UiMessageBox.Show("Your name: " + (value ?? "Canceled"));
        });
    }

    public void OnTestDialogBoxClick()
    {
        var handle = UiManager.Instance.ShowModalRoot<TestDialogBox>("Input your name?");
        handle.Hidden += (dlg, val) =>
        {
            if (val != null)
                UiMessageBox.Show("Name: " + val);
            else
                UiMessageBox.Show("Canceled");
        };
    }
}
