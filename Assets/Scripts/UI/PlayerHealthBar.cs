using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    [SerializeField]
    private Image _hpBar;
    [SerializeField]
    private Text _hpText;

    public void SetHealth(float hp, float maxHP)
    {
        _hpBar.fillAmount = Mathf.Clamp(hp / maxHP, 0, 1);

        if (hp > maxHP)
        {
            _hpText.text = string.Format("+{0}", (hp - maxHP));
        }
        else
        {
            _hpText.text = string.Format("{0} / {1}", hp, maxHP);
        }
    }
}
