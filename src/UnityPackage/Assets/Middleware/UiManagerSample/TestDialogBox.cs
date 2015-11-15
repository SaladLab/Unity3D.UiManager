using UnityEngine;
using UnityEngine.UI;

public class TestDialogBox : UiDialog
{
    public Text Message;
    public InputField Input;

    public override void OnShow(object param)
    {
        Message.text = (string)param;
        Input.text = "";
    }

    public void OnOkButtonClick()
    {
        Hide(Input.text);
    }
}
