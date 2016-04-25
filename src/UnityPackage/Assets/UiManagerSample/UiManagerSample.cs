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
        UiMessageBox.ShowMessageBox("This is a message <b>box</b>", _ =>
        {
            _logger.Info("MessageBox done");
        });
    }

    public void OnMessageQuestionBoxClick()
    {
        UiMessageBox.ShowQuestionBox("This is a message question <b>box</b>", UiMessageBox.QuestionType.OkCancel, r =>
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
            var handle = UiMessageBox.ShowQuestionBox("Do you want to continue?", UiMessageBox.QuestionType.ContinueStop);
            yield return StartCoroutine(handle.WaitForHide());
            _logger.Info(handle.ReturnValue);
            if ((UiMessageBox.QuestionResult)handle.ReturnValue == UiMessageBox.QuestionResult.Continue)
            {
                var handle2 = UiMessageBox.ShowMessageBox("Let's do it again");
                yield return StartCoroutine(handle2.WaitForHide());
            }
            else
            {
                UiMessageBox.ShowMessageBox("Done");
                yield break;
            }
        }
    }

    public void OnTestDialogBoxClick()
    {
        var handle = UiManager.Instance.ShowModalRoot<TestDialogBox>("Input your name?");
        handle.Hidden += (dlg, val) =>
        {
            if (val != null)
                UiMessageBox.ShowMessageBox("Name: " + val);
            else
                UiMessageBox.ShowMessageBox("Canceled");
        };
    }
}
