using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {
    [SerializeField]
    private GameObject _itemList;
    [SerializeField]
    private GameObject _dialogueBox;
    [SerializeField]
    private Text _dialogueText;
    [SerializeField]
    private string[] _firstTimeDialogue;
    private int _dialogueIndex;

    [SerializeField]
    private GameObject _bpWarning;

    [SerializeField]
    private Text _bpText;

    [SerializeField]
    private Button _close;

    // item buttons
    [SerializeField]
    private Button _katanaButton;
    [SerializeField]
    private Button _pistolButton;
    [SerializeField]
    private Button _shotgunButton;
    [SerializeField]
    private Button _pistolAmmoButton;
    [SerializeField]
    private Button _shotgunAmmoButton;

    // Use this for initialization
    void Start () {
        PlayerModel.IsInShop = true;
		if (!PlayerModel.HasVisitedShop)
        {
            PlayerModel.HasVisitedShop = true;
            RemoveItemBox();
            StartCoroutine(FirstTimeDialogue());
            return;
        }
        else
        {
            RemoveDialogueBox();
        }

        _bpWarning.SetActive(false);

        // close
        _close.onClick.AddListener(OnClickClose);

        // Button set up
        _pistolAmmoButton.onClick.AddListener(OnClickPistolAmmo);
        _shotgunAmmoButton.onClick.AddListener(OnClickShotgunAmmo);

        // remove items you have already
        if (PlayerModel.HasWeapon(WeaponLookup.NinjaSword))
        {
            Destroy(_katanaButton.gameObject);
        }
        else
        {
            _katanaButton.onClick.AddListener(OnClickKatana);
        }

        if (PlayerModel.HasWeapon(WeaponLookup.Pistol))
        {
            Destroy(_pistolButton.gameObject);
        }
        else
        {
            _pistolButton.onClick.AddListener(OnClickPistol);
        }

        if (PlayerModel.HasWeapon(WeaponLookup.Shotgun))
        {
            Destroy(_shotgunButton.gameObject);
        }
        else
        {
            _shotgunButton.onClick.AddListener(OnClickShotgun);
        }
    }

    IEnumerator FirstTimeDialogue()
    {
        _dialogueIndex = 0;
        do
        {
            _dialogueText.text = _firstTimeDialogue[_dialogueIndex];

            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    break;

                yield return null;
            }

            // Custom bat logic
            if (_dialogueIndex == 3)
            {
                Player player = FindObjectOfType<Player>();
                Human playerHuman = player.GetComponent<Human>();
                playerHuman.RemoveHeath(5);
                PlayerModel.AddWeapon(WeaponLookup.BaseballBat);
            }

            ++_dialogueIndex;
            yield return null;
        } while (_dialogueIndex < _firstTimeDialogue.Length);
        OnClickClose();
    }

    private void RemoveDialogueBox()
    {
        Destroy(_dialogueBox);
    }

    private void RemoveItemBox()
    {
        Destroy(_close.gameObject);
        Destroy(_itemList);
    }

    // close
    private void OnClickClose()
    {
        SceneManager.UnloadSceneAsync("Shop");
        PlayerModel.IsInShop = false;
    }
	
    // Buy buttons
    private void OnClickKatana()
    {
        if (AttemptPurchase(WeaponLookup.NinjaSword, 35))
            Destroy(_katanaButton.gameObject);
    }

    private void OnClickPistol()
    {
        if (AttemptPurchase(WeaponLookup.Pistol, 100))
            Destroy(_pistolButton.gameObject);
    }

    private void OnClickShotgun()
    {
        if (AttemptPurchase(WeaponLookup.Shotgun, 200))
            Destroy(_shotgunButton.gameObject);
    }

    private void OnClickPistolAmmo()
    {
        AttemptPurchase("PistolAmmo", 20);
    }

    private void OnClickShotgunAmmo()
    {
        AttemptPurchase("ShotgunAmmo", 25);
    }

    private bool AttemptPurchase(string item, int bpCost)
    {
        if (bpCost > PlayerModel.GetHealth())
        {
            // NOPE!
            StartCoroutine(ShowBPWarning());
            return false;
        }
        else
        {
            switch (item)
            {
                case "NinjaSword":
                    PlayerModel.AddWeapon(WeaponLookup.NinjaSword);
                    FindObjectOfType<Player>().GetComponent<Human>().RemoveHeath(bpCost);
                    ItemNotification.GetInstance().Notify("Katana");
                    break;

                case "Pistol":
                    PlayerModel.AddWeapon(WeaponLookup.Pistol);
                    FindObjectOfType<Player>().GetComponent<Human>().RemoveHeath(bpCost);
                    ItemNotification.GetInstance().Notify("Pistol");
                    break;

                case "Shotgun":
                    PlayerModel.AddWeapon(WeaponLookup.Shotgun);
                    FindObjectOfType<Player>().GetComponent<Human>().RemoveHeath(bpCost);
                    ItemNotification.GetInstance().Notify("Shotgun");
                    break;

                case "PistolAmmo":
                    PlayerModel.AddAmmo("PistolAmmo", 10);
                    FindObjectOfType<Player>().GetComponent<Human>().RemoveHeath(bpCost);
                    ItemNotification.GetInstance().Notify("+10 Pistol Ammo");
                    break;

                case "ShotgunAmmo":
                    PlayerModel.AddAmmo("ShotgunAmmo", 10);
                    FindObjectOfType<Player>().GetComponent<Human>().RemoveHeath(bpCost);
                    ItemNotification.GetInstance().Notify("+10 Shotgun Ammo");
                    break;
            }
        }

        return true;
    }

    private IEnumerator ShowBPWarning()
    {
        _bpWarning.SetActive(true);
        yield return new WaitForSeconds(2);
        _bpWarning.SetActive(false);
    }

    private void Update()
    {
        if (_bpText != null)
            _bpText.text = "BP: " + PlayerModel.GetHealth();
    }
}
