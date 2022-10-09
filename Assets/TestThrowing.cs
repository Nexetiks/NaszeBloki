using UnityEngine;

public class TestThrowing : MonoBehaviour
{
    public GameObject objectToThrow = null;
    public GameObject Kosciol = null;
    public GameObject Papaj = null;
    public GameObject Sebix = null;
    public GameObject Moher = null;
    GameObject ThrowedBox = null;

    public void ThrowTest(GameObject _thrower, GameObject _recever, GameManager.FractionEnum _fraction, UnitsData.PawnTypeEnum _pawnType)
    {
        if (_fraction == GameManager.FractionEnum.Sebix)
        {
            ThrowedBox = Instantiate(Sebix, _thrower.transform.position, Sebix.transform.rotation);
        }
        else if (_fraction == GameManager.FractionEnum.Moher)
        {
            ThrowedBox = Instantiate(Moher, _thrower.transform.position, Moher.transform.rotation);
        }
        else if (_fraction == GameManager.FractionEnum.Kler && _pawnType == UnitsData.PawnTypeEnum.Special)
        {
            ThrowedBox = Instantiate(Papaj, _thrower.transform.position, Papaj.transform.rotation);
        }
        else if (_fraction == GameManager.FractionEnum.Kler)
        {
            ThrowedBox = Instantiate(Kosciol, _thrower.transform.position, Kosciol.transform.rotation);
            ThrowedBox.transform.position = new Vector3(ThrowedBox.transform.position.x, ThrowedBox.transform.position.y + 1, ThrowedBox.transform.position.z);
            ThrowedBox.transform.rotation = Quaternion.Euler(0, 0, -90);
        }


        Rigidbody _addedRigittBody = ThrowedBox.AddComponent<Rigidbody>();
        ThrowedBox.transform.position = new Vector3(ThrowedBox.transform.position.x, ThrowedBox.transform.position.y + 1, ThrowedBox.transform.position.z);
        Vector3 _force = _recever.transform.position - _thrower.transform.position;
        _addedRigittBody.AddForce(_force * 100, ForceMode.Force);
        _addedRigittBody.useGravity = false;
        ThrowedBox.AddComponent<ThrowedObject>().enemy = _recever.transform.position;
    }
}
