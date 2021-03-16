using UnityEngine;

public class ChestInteraction : Interactable
{
    [SerializeField] private GameObject _item;
    [SerializeField] private int _nuberOfItems;
    [SerializeField] private int _forceRange;

    private GameObject[] _itemInstance;
    private float[] _xAxis;
    private float[] _zAxis;


    public void Start()
    {
        _itemInstance = new GameObject[_nuberOfItems];
        _xAxis = new float[_nuberOfItems];
        _zAxis = new float[_nuberOfItems];

        for (int i = 0; i < _nuberOfItems; i++)
        {
            _xAxis[i] = Random.Range(-_forceRange, _forceRange);
            _zAxis[i] = Random.Range(-_forceRange, _forceRange);
        }
    }
    public override void Interact()
    {
        for (int i = 0; i < _nuberOfItems; i++)
        {
            _itemInstance[i] = Instantiate(_item, transform.position, Quaternion.identity);
            
            _itemInstance[i].GetComponent<Rigidbody>().AddForce(new Vector3(_xAxis[i], 0, _zAxis[i]), ForceMode.Impulse);      
        }
        Destroy(this.gameObject);
    }

}
