using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIButton : MonoBehaviour
{
    private InventoryItem _itemData;
    public InventoryItem ItemData 
    {
        get
        { 
            return _itemData; 
        }
        set 
        {
            _itemData = value;
        }
    }

    [SerializeField] private Image _image;
    [SerializeField] private Text _labelText;
    [SerializeField] private Text _amountText;
    [SerializeField] private List<Sprite> _sprites;

    private InventoryUsedCallback _callback;
    public InventoryUsedCallback Callback 
    {
        get 
        {
            return _callback;
        }
        set 
        {
            _callback = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _labelText.text = _itemData.CrystallType.ToString();
        _amountText.text = _itemData.Quantity.ToString();
        string spriteNameToSearch = _itemData.CrystallType.ToString().ToLower();
        _image.sprite = _sprites.Find(x => x.name.Contains(spriteNameToSearch));
        _labelText.text = spriteNameToSearch;
        _amountText.text = _itemData.Quantity.ToString();

        gameObject.GetComponent<Button>().onClick.AddListener(() => _callback(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
