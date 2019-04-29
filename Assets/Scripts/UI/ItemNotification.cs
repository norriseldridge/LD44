using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotification : MonoBehaviour {
    [SerializeField]
    private Image _notificationImage;
    [SerializeField]
    private Image _start;
    [SerializeField]
    private Image _end;

    [SerializeField]
    private Text _itemText;

    public static ItemNotification GetInstance()
    {
        return FindObjectOfType<ItemNotification>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Notify(string item)
    {
        _itemText.text = string.Format("{0} Added!", item);
        StartCoroutine(Tween());
    }

    private IEnumerator Tween()
    {
        _notificationImage.transform.position = _start.transform.position;

        for (int i = 0; i < 30; ++i)
        {
            _notificationImage.transform.position = Vector3.Lerp(_notificationImage.transform.position, _end.transform.position, i * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < 30; ++i)
        {
            _notificationImage.transform.position = Vector3.Lerp(_notificationImage.transform.position, _start.transform.position, i * Time.deltaTime);
            yield return null;
        }
    }
}
