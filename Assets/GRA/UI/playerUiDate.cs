using UnityEngine;
using TMPro;
public class playerUiDate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] gamewordDate _date;

    void Start()
    {
        _date.OnDateChange += DateUpdate;
        _text.text="data";
        DateUpdate();
    }
    void DateUpdate()
    {
          _text.text="dzień: "+_date.getDays();
    }
}
